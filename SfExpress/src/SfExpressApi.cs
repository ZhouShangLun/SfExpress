using Abp.Configuration;
using Abp.Dependency;
using Castle.Core.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SfExpress.src.SfRequest;
using SfExpress.src.SfResponse;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SfExpress.src
{
    public interface ISfExpressApi : ITransientDependency
    {
        /// <summary>
        /// 顺丰寄件
        /// </summary>
        Task<SendOrderResponse> SendExpress(SendOrderInfo info);

        /// <summary>
        /// 顺丰打印接口
        /// </summary>
        Task<string> SfPrintRequest(SendPrintInfo info);

        /// <summary>
        /// 取消顺丰寄件订单
        /// </summary>
        /// <param name="orderId">顺丰订单Id</param>
        Task<bool> SfCancelOrder(string orderId);
    }

    public class SfExpressApi : ISfExpressApi
    {
        private readonly HttpClient _httpClient;
        private readonly ISettingManager _settingManager;
        public ILogger Logger { get; set; }

        public SfExpressApi(
            IHttpClientFactory httpClientFactory,
            ISettingManager settingManager)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(SfExpressApi));
            Logger = NullLogger.Instance;
            _settingManager = settingManager;
        }

        public async Task<SendOrderResponse> SendExpress(SendOrderInfo info)
        {
            Logger.InfoFormat("正在向顺丰发起寄件，订单号:{0},收件人:{1},收货人联系手机:{2},收货地址:{3},寄件人:{4},寄件地址:{5}"
                , info.OrderId, info.DContact, info.DMobile, info.DAddress, info.JContact, info.JAddress);

            SendOrderRequest request = new SendOrderRequest();
            request.OrderBody.Order = info;
            request.Head = await _settingManager.GetSettingValueAsync(Settings.ShunFeng.ClientCode);
            request.OrderBody.Order.CustId = await _settingManager.GetSettingValueAsync(Settings.ShunFeng.CustId);
            request.ServiceName = "OrderService";
            request.Lang = "zh-CN";
            var url = await _settingManager.GetSettingValueAsync(Settings.ShunFeng.Url);
            var checkword = await _settingManager.GetSettingValueAsync(Settings.ShunFeng.CheckWord);
            var result = await DoPostAsync(
                    url,
                    request.ToXml(), GetVerifyCode(request.ToXml(), checkword));
            var response = SendOrderResponse.Parse(result);

            if (response.Head == "OK")
            {
                Logger.InfoFormat("订单号:{0}发起寄件成功!运单号:{1}。", response.Body.OrderResponse.OrderId, response.Body.OrderResponse.MailNo);
            }
            else
            {
                Logger.ErrorFormat("订单号:{0}发起寄件失败，失败原因:{1}", info.OrderId, response.Error);
            }

            return response;
        }


        private async Task<string> DoPostAsync(string url, string content, string verifyCode)
        {
            string str;
            Encoding myEncoding = Encoding.GetEncoding("UTF-8");  //确定中文编码方式。
            string param = HttpUtility.UrlEncode("xml", myEncoding) + "=" + HttpUtility.UrlEncode(content, myEncoding) + "&" + HttpUtility.UrlEncode("verifyCode", myEncoding) + "=" + HttpUtility.UrlEncode(verifyCode, myEncoding);
            var response = await _httpClient.PostAsync(url, new StringContent(param, Encoding.UTF8, "application/x-www-form-urlencoded"));
            response.EnsureSuccessStatusCode();
            str = await response.Content.ReadAsStringAsync();

            return str;
        }
        private string GetVerifyCode(string xml, string code)
        {
            string param = $"{xml}{code}";

            return Convert.ToBase64String(GetMd5_16byte(param));
        }

        private byte[] GetMd5_16byte(string ConvertString)
        {
            //使用加密服务提供程序
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            //将指定的字节子数组的每个元素的数值转换为它的等效十六进制字符串表示形式。
            return md5.ComputeHash(Encoding.Default.GetBytes(ConvertString));
        }


        public async Task<string> SfPrintRequest(SendPrintInfo info)
        {
            var sfPrintRequest = new SfPrintRequest
            {
                AppId = await _settingManager.GetSettingValueAsync(Settings.ShunFeng.ClientCode),
                AppKey = await _settingManager.GetSettingValueAsync(Settings.ShunFeng.CheckWord),
                MailNo = info.MailNo,
                ExpressType = ExpressType.SfEconomyExpress,
                PayMethod = info.PayMethod,
                DestCode = info.DestCode,
                DeliverCompany = info.JCompany,
                DeliverName = info.JContact,
                DeliverAddress = info.JAddress,
                DeliverMobile = info.JMobile,
                ConsignerName = info.DContact,
                ConsignerMobile = info.DMobile,
                ConsignerAddress = info.DAddress,
                EncryptMobile = true,
                EncryptCustName = false,
            };
            var rlsInfo = new RlsInfo
            {
                DestRouteLabel = info.DestRouteLabel,
                ProCode = "T6",
                Qrcode = info.TwoDimensionCode,
                WaybillNo = info.MailNo,
                PrintIcon = "00000000"
            };
            //目前发货信息的items是写死的 [***]* 1  如果想变成详细信息，直接赋值info.Items
            sfPrintRequest.CargoInfoDtoList = new List<CargoInfo>() { new CargoInfo { Cargo = "[***]*", CargoCount = "1" } };
            sfPrintRequest.RlsInfoDtoList.Add(rlsInfo);

            var printUrl = await _settingManager.GetSettingValueAsync(Settings.ShunFeng.PrintUrl);
            var responses = await _httpClient.PostAsync(printUrl,
               new StringContent(JsonConvert.SerializeObject(new List<object> { sfPrintRequest }, new JsonSerializerSettings()
               {
                   ContractResolver = new CamelCasePropertyNamesContractResolver()
               })));
            responses.EnsureSuccessStatusCode();
            var httpResult = await responses.Content.ReadAsStringAsync();
            var bytes = GetPictureBytes(httpResult);
            var picString = Convert.ToBase64String(bytes);
            return picString;
        }

        private byte[] GetPictureBytes(string httpResult)
        {
            if (httpResult.Contains("["))
            {
                httpResult = httpResult.Substring(httpResult.IndexOf("[") + 1, httpResult.IndexOf("]") - httpResult.IndexOf("[") - 1);
            }

            if (httpResult.StartsWith("\""))
            {

                httpResult = httpResult.Substring(1, httpResult.Length - 1);
            }
            if (httpResult.EndsWith("\""))
            {
                httpResult = httpResult.Substring(0, httpResult.Length - 1);
            }

            // 将换行全部替换成空
            httpResult = httpResult.Replace("\\n", "");
            var bytes = Convert.FromBase64String(httpResult);
            int x = 256;
            byte a = (byte)x;

            for (int i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] < 0)
                {
                    bytes[i] += a;
                }
            }
            return bytes;

        }

        public async Task<bool> SfCancelOrder(string orderId)
        {
            Logger.InfoFormat("正在向顺丰取消订单，订单号:{0}", orderId);

            CancelOrderRequest request = new CancelOrderRequest();
            request.Head = await _settingManager.GetSettingValueAsync(Settings.ShunFeng.ClientCode);
            request.OrderConfirm.OrderId = orderId;
            request.OrderConfirm.DealType = "2";
            request.ServiceName = "OrderConfirmService";
            request.Lang = "zh-CN";

            var url = await _settingManager.GetSettingValueAsync(Settings.ShunFeng.Url);
            var checkword = await _settingManager.GetSettingValueAsync(Settings.ShunFeng.CheckWord);
            var result = await DoPostAsync(url, request.ToXml(), GetVerifyCode(request.ToXml(), checkword));
            var response = CancelOrderResponse.Parse(result);

            if (response.Head == "OK")
            {
                if (response.Body.ConfirmResponse.ResStatus == "2")
                {
                    Logger.InfoFormat("订单号:{0}取消寄件成功!", orderId);
                    return true;
                }
                else
                {
                    Logger.ErrorFormat("订单号:{0}取消寄件失败，失败原因:{1}", orderId, "客户订单号与顺丰运单不匹配");
                    return false;
                }
            }
            else
            {
                if (response.Error == "已消单")
                {
                    Logger.InfoFormat("订单号:{0}已消单", orderId);
                    return true;
                }
                else
                {
                    Logger.ErrorFormat("订单号:{0}取消寄件失败，失败原因:{1}", orderId, response.Error);
                    return false;
                }
            }
        }
    }
}
