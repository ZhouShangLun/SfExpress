using System.Collections.Generic;

namespace SfExpress.src.SfRequest
{
    public class SendPrintInfo
    {
        /// <summary>
        /// 寄件公司
        /// </summary>
        public string JCompany { get; set; }
        /// <summary>
        /// 收件地代码
        /// </summary>
        public string DestCode { get; set; }

        /// <summary>
        ///     寄件方联系人，如果需要生成电子运单，则为必填。
        /// </summary>
        public string JContact { get; set; }

        /// <summary>
        ///     寄件方手机
        /// </summary>
        public string JMobile { get; set; }

        /// <summary>
        ///     寄件方详细地址
        /// </summary>
        public string JAddress { get; set; }

        /// <summary>
        ///     收件方联系人
        /// </summary>
        public string DContact { get; set; }

        /// <summary>
        ///     收件人详细地址
        /// </summary>
        public string DAddress { get; set; }


        /// <summary>
        ///     收件人手机
        /// </summary>
        public string DMobile { get; set; }


        /// <summary>
        /// 顺丰运单号
        /// </summary>
        public string MailNo { get; set; }


        /// <summary>
        /// 终点路由标签(打印相关字段)
        /// </summary>
        public string DestRouteLabel { get; set; }


        /// <summary>
        /// 二维码(打印相关字段)
        /// </summary>
        public string TwoDimensionCode { get; set; }

        public SfPayMethod PayMethod { get; set; }

        public ICollection<CargoInfo> Items { get; set; }
    }
}
