using System.Xml;
using System.Xml.Serialization;

namespace SfExpress.src.SfRequest
{
    [XmlRoot("Request")]
    public class SendOrderRequest : SendRequestBase
    {
        [XmlElement("Body")]
        public OrderBody OrderBody { get; set; }

        public SendOrderRequest()
        {
            OrderBody = new OrderBody();
        }

        public string ToXml()
        {
            return base.ToXml(typeof(SendOrderRequest));
        }
    }

    public class OrderBody
    {
        [XmlElement("Order")]
        public SendOrderInfo Order { get; set; }

        public OrderBody()
        {
            Order = new SendOrderInfo();
        }
    }

    public class SendOrderInfo
    {
        /// <summary>
        /// 客户订单号，最大长度限于 56 位，该字段客户可自行定义，请尽量命名的规范有意义，如SFAPI20160830001，订单号作为客户下单的凭证，不允许重复提交订单号。
        /// </summary>
        [XmlAttribute("orderid")]
        public string OrderId { get; set; }

        /// <summary>
        ///     运单号 顺丰运单号，一个订单只能有一个母单号，如果是子母单的情况，以半角逗号分隔，主单号在第一个位置，如 “755123456789,001123456789,002123456789” ，
        ///     对于路由推送注册，此字段为必填。
        /// </summary>
        [XmlAttribute("mailno")]
        public string MailNo { get; set; }

        /// <summary>
        ///     寄件方公司名称，如果需要 生成电子运单，则为必填。
        /// </summary>
        [XmlAttribute("j_company")]
        public string JCompany { get; set; }

        /// <summary>
        ///     寄件方联系人，如果需要生成电子运单，则为必填。
        /// </summary>
        [XmlAttribute("j_contact")]
        public string JContact { get; set; }

        /// <summary>
        ///     寄件方联系电话，如果需要生成电子运单，则为必填。
        /// </summary>
        [XmlAttribute("j_tel")]
        public string JTel { get; set; }

        /// <summary>
        ///     寄件方手机
        /// </summary>
        [XmlAttribute("j_mobile")]
        public string JMobile { get; set; }

        /// <summary>
        ///     寄件方详细地址
        /// </summary>
        [XmlAttribute("j_address")]
        public string JAddress { get; set; }

        /// <summary>
        ///     到件方公司名称
        /// </summary>
        [XmlAttribute("d_company")]
        public string DCompany { get; set; }

        /// <summary>
        ///     收件方联系人
        /// </summary>
        [XmlAttribute("d_contact")]
        public string DContact { get; set; }

        /// <summary>
        ///     收件人联系电话
        /// </summary>
        [XmlAttribute("d_tel")]
        public string DTel { get; set; }

        /// <summary>
        ///     收件人手机
        /// </summary>
        [XmlAttribute("d_mobile")]
        public string DMobile { get; set; }

        /// <summary>
        ///     收件人详细地址
        /// </summary>
        [XmlAttribute("d_address")]
        public string DAddress { get; set; }

        /// <summary>
        ///     包裹数(1个包裹对应一个运单号)
        /// </summary>
        [XmlAttribute("parcel_quantity")]
        public int ParcelQuantity { get; set; }

        /// <summary>
        ///     快件产品类别（只有再商务上与顺丰约定的类别方可使用）
        /// </summary>
        [XmlAttribute("express_type")]
        public string ExpressType { get; set; }

        /// <summary>
        ///     顺丰月结卡号
        /// </summary>
        [XmlAttribute("custid")]
        public string CustId { get; set; }

        /// <summary>
        /// 付款方式  1:寄方付2:收方付3:第三方付  默认为1
        /// </summary>
        [XmlAttribute("pay_method")]
        public string PayMethod { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        [XmlAttribute("remark")]
        public string Remark { get; set; }


        /// <summary>
        ///     订单元素
        /// </summary>
        [XmlElement("Cargo")]
        public OrderCargo OrderCargos { get; set; }

    }

    public class OrderCargo
    {
        /// <summary>
        ///     货物名称
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

    }

}
