using System.Xml.Serialization;

namespace EmailSender.Domain.Models
{
    [XmlRoot(ElementName = "tempalate")]
    public class Tempalate
    {

        [XmlElement(ElementName = "name")]
        public string? Name { get; set; }

        [XmlElement(ElementName = "marketingData")]
        public string? MarketingData { get; set; }

        [XmlAttribute(AttributeName = "id")]
        public int Id { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "client")]
    public class Client
    {

        [XmlElement(ElementName = "tempalate")]
        public Tempalate? Tempalate { get; set; }

        [XmlAttribute(AttributeName = "id")]
        public int Id { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "clients")]
    public class RootClients
    {
        [XmlElement(ElementName = "clients")]
        public ClientElement? Clients { get; set; }
    }

    [XmlRoot(ElementName = "client")]

    public class ClientElement
    {
        [XmlElement(ElementName = "client")]
        public List<Client>? Client { get; set; }
    }


}