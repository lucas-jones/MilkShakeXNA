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

        public bool HasValue(string key)
        {
            return node.Attributes[key] != null;
        }

        public string GetValue(string key)
        {
            return (node.Attributes[key] != null) ? node.Attributes[key].Value : null;
        }

        public string GetValueOrDefault(string key, string _default)
        {
            return (node.Attributes[key] != null) ? node.Attributes[key].Value : _default;
        }

        public T GetValue<T>(string key)
        {
            string value = node.Attributes[key].Value;
            return (T)Convert.ChangeType(value, typeof(T));
        }

        public T GetValueOrDefault<T>(string key, T _default)
        {
            return (node.Attributes[key] != null) ? (T)Convert.ChangeType(node.Attributes[key].Value, typeof(T)) : _default;
        }

    }
}
