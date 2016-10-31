﻿using System;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace Mews.Eet.Soap.Signing
{
    public class SecurityTokenReference : KeyInfoClause
    {
        public SecurityTokenReference(string binarySecurityToken)
        {
            BinarySecurityTokenId = binarySecurityToken;
        }

        public string BinarySecurityTokenId { get; set; }

        public override XmlElement GetXml()
        {
            XmlDocument doc = new XmlDocument();

            XmlElement strXmlElement = doc.CreateElement("wse", "SecurityTokenReference", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");

            doc.AppendChild(strXmlElement);

            XmlElement reference = doc.CreateElement("wse", "Reference", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
            reference.SetAttribute("URI", "#" + BinarySecurityTokenId);
            reference.SetAttribute("ValueType", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509");

            strXmlElement.AppendChild(reference);

            return strXmlElement;
        }

        public override void LoadXml(XmlElement element)
        {
            throw new NotImplementedException();
        }
    }
}