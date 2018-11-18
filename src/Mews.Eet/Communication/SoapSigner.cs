using System;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;
using Mews.Eet.Dto;
using Mews.Eet.Extensions;

namespace Mews.Eet.Communication
{
    public enum SignAlgorithm
    {
        Sha1 = 0,
        Sha256 = 1
    }

    public class SoapSigner
    {
        public SoapSigner(Certificate certificate, SignAlgorithm signAlgorithm)
        {
            Certificate = certificate;
            SecurityToken = Convert.ToBase64String(certificate.X509Certificate2.GetRawCertData());
            SignatureMethod = GetSignatureMethodUri(signAlgorithm);
            DigestMethod = GetDigestMethod(signAlgorithm);
            RsaKey = GetRsaKey(signAlgorithm, certificate.X509Certificate2);
        }

        private string SecurityToken { get; }

        private string SignatureMethod { get; }

        private string DigestMethod { get; }

        private RSACryptoServiceProvider RsaKey { get; }

        private Certificate Certificate { get; }

        public XmlDocument SignMessage(XmlDocument xmlDoc)
        {
            var namespaceManager = new XmlNamespaceManager(xmlDoc.NameTable);
            namespaceManager.AddNamespace("s", "http://schemas.xmlsoap.org/soap/envelope/");
            namespaceManager.AddNamespace("wsu", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");

            var soapHeaderNode = xmlDoc.DocumentElement.SelectSingleNode("//s:Header", namespaceManager) as XmlElement;
            var bodyNode = xmlDoc.DocumentElement.SelectSingleNode("//s:Body", namespaceManager) as XmlElement;

            if (bodyNode == null)
            {
                throw new Exception("No body tag found.");
            }

            var securityNode = xmlDoc.CreateElement(
                prefix: "wsse",
                localName: "Security",
                namespaceURI: "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"
            );

            var binarySecurityTokenElement = xmlDoc.CreateElement("wse", "BinarySecurityToken", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
            binarySecurityTokenElement.SetAttribute("EncodingType", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary");
            binarySecurityTokenElement.SetAttribute("ValueType", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3");
            binarySecurityTokenElement.SetAttribute("Id", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd", "BinaryToken1");
            binarySecurityTokenElement.InnerText = SecurityToken;

            securityNode.AppendChild(binarySecurityTokenElement);
            soapHeaderNode.AppendChild(securityNode);

            var signedXml = new SignedXmlWithId(xmlDoc);
            signedXml.SignedInfo.SignatureMethod = SignatureMethod;
            signedXml.SigningKey = RsaKey;

            var keyInfo = new KeyInfo();
            keyInfo.AddClause(new SecurityTokenReference("BinaryToken1"));
            signedXml.KeyInfo = keyInfo;

            signedXml.SignedInfo.CanonicalizationMethod = SignedXml.XmlDsigExcC14NTransformUrl;

            var reference = new Reference();
            reference.Uri = "#_1";
            reference.DigestMethod = DigestMethod;

            reference.AddTransform(new XmlDsigExcC14NTransform());
            signedXml.AddReference(reference);
            signedXml.ComputeSignature();

            var signedElement = signedXml.GetXml();
            securityNode.AppendChild(signedElement);

            return xmlDoc;
        }

        private RSACryptoServiceProvider GetRsaKey(SignAlgorithm signAlgorithm, X509Certificate2 certificate)
        {
            if (signAlgorithm == SignAlgorithm.Sha1)
            {
                return certificate.PrivateKey as RSACryptoServiceProvider;
            }

            if (signAlgorithm == SignAlgorithm.Sha256)
            {
				return certificate.GetPrivateKeyRsaCryptoServiceProvider(Certificate.UseMachineKeyStore);
            }

            throw new InvalidEnumArgumentException($"Unsupported signing algorithm {signAlgorithm}.");
        }

        private string GetSignatureMethodUri(SignAlgorithm signAlgorithm)
        {
            if (signAlgorithm == SignAlgorithm.Sha1)
            {
                return "http://www.w3.org/2000/09/xmldsig#rsa-sha1";
            }
            if (signAlgorithm == SignAlgorithm.Sha256)
            {
                return "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";
            }

            throw new InvalidEnumArgumentException($"Unsupported signing algorithm {signAlgorithm}.");
        }

        private string GetDigestMethod(SignAlgorithm signAlgorithm)
        {
            if (signAlgorithm == SignAlgorithm.Sha1)
            {
                return "http://www.w3.org/2000/09/xmldsig#sha1";
            }
            if (signAlgorithm == SignAlgorithm.Sha256)
            {
                return "http://www.w3.org/2001/04/xmlenc#sha256";
            }

            throw new InvalidEnumArgumentException($"Unsupported signing algorithm {signAlgorithm}.");
        }
    }
}