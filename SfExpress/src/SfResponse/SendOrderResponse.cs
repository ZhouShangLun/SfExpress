using System;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SfExpress.src.SfResponse
{
    public class SendOrderResponse
    {
        [XmlElement("Body")]
        public OrderResponseBody Body { get; set; }


        public SendOrderResponse()
        {
            Body = new OrderResponseBody();
        }

        public string Head { get; set; }

        [XmlElement("ERROR")]
        public string Error { get; set; }

        public static SendOrderResponse Parse(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentNullException(nameof(content));
            }
            SendOrderResponse rsp = new SendOrderResponse();

            var bodyDoc = XDocument.Parse(content);

            using (var sr = new StringReader(content))
            {
                var rootAttribute = new XmlRootAttribute();
                rootAttribute.ElementName = "Response";
                rootAttribute.IsNullable = true;
                var xmldes = new XmlSerializer(typeof(SendOrderResponse), rootAttribute);
                rsp = (SendOrderResponse)xmldes.Deserialize(sr);
            }
            if (rsp.Head != "OK" && string.IsNullOrEmpty(rsp.Error))
            {
                rsp.Error = content;
            }

            return rsp;
        }
    }
    public class OrderResponseBody
    {
        [XmlElement("OrderResponse")]
        public SendOrderResponseBody OrderResponse { get; set; }

        public OrderResponseBody()
        {
            OrderResponse = new SendOrderResponseBody();
        }
    }

    public class SendOrderResponseBody
    {
        /// <summary>
        ///  客户订单号
        /// </summary>
        [XmlAttribute("orderid")]
        public string OrderId { get; set; }

        /// <summary>
        /// 顺丰运单号
        /// </summary>
        [XmlAttribute("mailno")]
        public string MailNo { get; set; }

        [XmlAttribute("return_tracking_no")]
        public string ReturnTrackingNo { get; set; }

        /// <summary>
        /// 筛单结果：1：人工确认2：可收派 3：不可以收派
        /// </summary>
        [XmlAttribute("filter_result")]
        public string FilterResult { get; set; }

        /// <summary>
        /// 如果filter_result=3时为必填，不可以收派的原因代码：1：收方超范围 2：派方超范围3-：其它原因
        /// </summary>
        [XmlAttribute("remark")]
        public string Remark { get; set; }

        [XmlElement("rls_info")]
        public Rls_Info RlsInfo { get; set; }
    }

    public class Rls_Info
    {
        [XmlAttribute("rls_errormsg")]
        public string RlsErrormsg { get; set; }

        [XmlAttribute("invoke_result")]
        public string InvokeResult { get; set; }

        [XmlAttribute("rls_code")]
        public string RlsCode { get; set; }

        [XmlElement("rls_detail")]
        public RlsDetail RlsDetail { get; set; }
    }

    public class RlsDetail
    {

        /// <summary>
        /// 终点路由标签(打印相关字段)
        /// </summary>
        [XmlAttribute("destRouteLabel")]
        public string DestRouteLabel { get; set; }

        /// <summary>
        /// 二维码(打印相关字段)
        /// </summary>
        [XmlAttribute("twoDimensionCode")]
        public string TwoDimensionCode { get; set; }
    }
}
