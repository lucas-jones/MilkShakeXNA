using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using MilkShakeFramework.IO.File.XML;

namespace MilkShakeFramework.IO.File.TMX.Components
{
    public class TMXProperties
    {
        public Dictionary<string, string> Data;

        public string this[string key]
        {
            get { return GetValue(key); }
        }

        public string GetValue(string key)
        {
            return (Data.ContainsKey(key)) ? Data[key] : "Unknown";
        }

        public string GetValueOrDefault(string key, string defaultValue)
        {
            return (Data.ContainsKey(key)) ? Data[key] : defaultValue;
        }

        public TMXProperties(XmlNode node)
        {
            Data = new Dictionary<string, string>();

            foreach (XmlNode propertie in node)
            {
                NodeReader reader = new NodeReader(propertie);
                Data.Add(reader.GetValue(TMXKeywords.NAME), reader.GetValue(TMXKeywords.VALUE));
            }
        }

        public TMXProperties()
        {
            Data = new Dictionary<string, string>();
        }

    }
}
