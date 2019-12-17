using System.Xml.Serialization;

namespace SfExpress.src.SfRequest
{
    [XmlRoot("Request")]
    public class CancelOrderRequest : SendRequestBase
    {
        [XmlElement("OrderConfirm")]
        public OrderConfirm OrderConfirm { get; set; }

        public CancelOrderRequest()
        {
            OrderConfirm = new OrderConfirm();
        }


        public string ToXml()
        {
            return base.ToXml(typeof(CancelOrderRequest));
        }
    }

    public class OrderConfirm
    {
        /// <summary>
        /// 客户订单号
        /// </summary>
        [XmlAttribute("orderid")]
        public string OrderId { get; set; }

        /// <summary>
        /// 客户订单操作标识:1,确认 2,取消
        /// </summary>
        [XmlAttribute("dealtype")]
        public string DealType { get; set; }
    }
}
