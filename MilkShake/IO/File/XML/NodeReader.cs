using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MilkShakeFramework.IO.File.XML
{
    public class NodeReader 
    {
        public XmlNode node;

        public NodeReader(XmlNode xmlNode)
        {
            node = xmlNode;
        }

        public string GetValue(string key)
        {
            return node.Attributes[key].Value;
        }

        public T GetValue<T>(string key)
        {
            string value = node.Attributes[key].Value;
            return (T)Convert.ChangeType(value, typeof(T));
        }

    }
}
