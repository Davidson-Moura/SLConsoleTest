
using System.Xml.Serialization;

namespace ConsoleTest
{
    [XmlType("Body")]
    public class Body
    {
        [System.Xml.Serialization.XmlElement("JournalEntryBulkCreateRequest", Namespace = "http://sap.com/xi/SAPSCORE/SFIN")]
        public JournalEntryCreateRequestBulkMessage Object { get; set; }
    }

    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class Envelope<T> where T : new()
    {
        public Envelope()
        {
            
        }
        public Envelope(T obj)
        {
            Body = obj;
        }

        [XmlElement(ElementName = "Header")]
        public BusinessDocumentMessageHeader Header { get; set; }

        [XmlElement(ElementName = "Body")]
        public T Body { get; set; }
    }


}
