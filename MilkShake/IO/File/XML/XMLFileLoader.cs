using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MilkShakeFramework.IO.File.XML
{
    public static class XMLFileLoader
    {
        public static XmlNode NodeFromFile(string xmlLocation)
        {
            XmlDocument document = new XmlDocument();
            document.Load(xmlLocation);

            return document.FirstChild; // Unsure
        }
    }
}
