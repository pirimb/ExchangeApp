using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace currency_exchange.Models
{
    /*public class Valute
    {
        [XmlElement("Nominal")]
        public string Nominal { get; set; }
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("Value")]
        public decimal Value { get; set; }
    }*/

    [XmlRoot(ElementName = "Valute")]
    public class Valute
    {

        [XmlElement(ElementName = "Nominal")]
        public string Nominal { get; set; }

        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "Value")]
        public double Value { get; set; }

        [XmlAttribute(AttributeName = "Code")]
        public string Code { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "ValType")]
    public class ValType
    {

        [XmlElement(ElementName = "Valute")]
        public List<Valute> Valute { get; set; }

        [XmlAttribute(AttributeName = "Type")]
        public string Type { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "ValCurs")]
    public class ValCurs
    {

        [XmlElement(ElementName = "ValType")]
        public List<ValType> ValType { get; set; }

        

        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "Description")]
        public string Description { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}
