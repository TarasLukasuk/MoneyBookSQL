using System.Windows;
using System.Xml;

namespace MoneyBookSQL
{
    class XML
    {
        public void SavePrice(string totalPrice)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("Price.config");

            XmlElement xRoot = xDoc.DocumentElement;

            XmlElement personElem = xDoc.CreateElement("Price");

            XmlAttribute attribute = xDoc.CreateAttribute("TotalPrice");
            attribute.Value = totalPrice;

            personElem.Attributes.Append(attribute);

            xRoot?.AppendChild(personElem);

            xDoc.Save("Price.config");
        }

        public int GetTotalPrice()
        {
            int result = 0;

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("Price.config");
            XmlElement xRoot = xDoc.DocumentElement;
            if (xRoot != null)
            {
                foreach (XmlElement xnode in xRoot)
                {
                   result= int.Parse(xnode.Attributes.GetNamedItem("TotalPrice").Value);
                }
            }

            return result;
        }
    }
}
