using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace SfExpress.src.SfRequest
{
    public class SendRequestBase
    {

        public string Head { get; set; }

        [XmlAttribute("lang")]
        public virtual string Lang { get; set; }


        [XmlAttribute("service")]
        public string ServiceName { get; set; }



        public virtual string ToXml(Type type)
        {
            XmlSerializer xsSubmit = new XmlSerializer(type);
            var xml = "";

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, this);
                    xml = sww.ToString();
                }
            }

            return xml;
        }
    }
}
