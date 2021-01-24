using CollegeCounter.Serializer.Implementation;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace CollegeCounter.Serializer.Interface
{
    public class MyXmlSerializer : IMySerializer
    {
        public string GetFileExtension()
        {
            return ".xml";
        }

        public string SerializeObject(object input)
        {
            XmlSerializer xsSubmit = new XmlSerializer(input.GetType());

            var xml = "";

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, input);
                    xml = sww.ToString();
                }
            }
            return xml;
        }
    }
}
