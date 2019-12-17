using System;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SfExpress.src.SfResponse
{
    public class CancelOrderResponse
    {
        [XmlElement("Body")]
        public OrderConfirmResponse Body { get; set; }


        public CancelOrderResponse()
        {
            Body = new OrderConfirmResponse();
        }

        public string Head { get; set; }

        [XmlElement("ERROR")]
        public string Error { get; set; }

        public static CancelOrderResponse Parse(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentNullException(nameof(content));
            }
            CancelOrderResponse rsp = new CancelOrderResponse();

            var bodyDoc = XDocument.Parse(content);

            using (var sr = new StringReader(content))
            {
                var rootAttribute = new XmlRootAttribute();
                rootAttribute.ElementName = "Response";
                rootAttribute.IsNullable = true;
                var xmldes = new XmlSerializer(typeof(CancelOrderResponse), rootAttribute);
                rsp = (CancelOrderResponse)xmldes.Deserialize(sr);
            }
            if (rsp.Head != "OK" && string.IsNullOrEmpty(rsp.Error))
            {
                rsp.Error = content;
            }

            return rsp;
        }
    }

    public class OrderConfirmResponse
    {
        [XmlElement("OrderConfirmResponse")]
        public OrderConfirmResponseBody ConfirmResponse { get; set; }

        public OrderConfirmResponse()
        {
            ConfirmResponse = new OrderConfirmResponseBody();
        }


    }

    public class OrderConfirmResponseBody
    {
        /// <summary>
        /// 客户订单号
        /// </summary>
        [XmlAttribute("orderid")]
        public string OrderId { get; set; }

        /// <summary>
        /// 1:客户订单号与顺丰运单不匹配    2 :操作成功
        /// </summary>
        [XmlAttribute("res_status")]
        public string ResStatus { get; set; }
    }
}
