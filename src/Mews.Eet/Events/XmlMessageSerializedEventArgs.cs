using System;
using System.Xml;

namespace Mews.Eet.Events
{
    public class XmlMessageSerializedEventArgs : EventArgs
    {
        public XmlMessageSerializedEventArgs(XmlElement xmlElement)
        {
            XmlElement = xmlElement;
        }

        public XmlElement XmlElement { get; }
    }
}
