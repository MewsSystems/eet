using System;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml;

namespace Mews.Eet.Communication
{
    public class SoapClient : IDisposable
    {
        private readonly SoapHttpClient httpClient;
        private readonly X509Certificate2 certificate;
        private readonly SignAlgorithm signAlgorithm;
        private readonly XmlManipulator xmlManipulator;

        public SoapClient(Uri endpointUri, X509Certificate2 certificate, SignAlgorithm signAlgorithm = SignAlgorithm.Sha256)
        {
            httpClient = new SoapHttpClient(endpointUri);
            this.certificate = certificate;
            this.signAlgorithm = signAlgorithm;
            xmlManipulator = new XmlManipulator();
        }

        public async Task<TOut> SendAsync<TIn, TOut>(TIn messageBodyObject, string operation)
            where TIn : class, new()
            where TOut : class, new()
        {
            var messageBodyXmlElement = xmlManipulator.Serialize(messageBodyObject);
            var soapMessage = new SoapMessage(new SoapMessagePart(messageBodyXmlElement));
            var xmlDocument = certificate == null ? soapMessage.GetXmlDocument() : soapMessage.GetSignedXmlDocument(certificate, signAlgorithm);

            var response = await httpClient.SendAsync(xmlDocument.OuterXml, operation).ConfigureAwait(false);
            var soapBody = GetSoapBody(response);

            return xmlManipulator.Deserialize<TOut>(soapBody);
        }

        public void Dispose()
        {
            httpClient.Dispose();
        }

        private XmlElement GetSoapBody(string soapXmlString)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(soapXmlString);

            var soapMessage = SoapMessage.FromSoapXml(xmlDocument);
            if (!soapMessage.VerifySignature())
            {
                throw new SecurityException("The SOAP message signature is not valid.");
            }
            return soapMessage.Body.XmlElement.FirstChild as XmlElement;
        }
    }
}
