using System;
using System.Threading.Tasks;
using Mews.Eet.Dto;
using Mews.Eet.Dto.Wsdl;

namespace Mews.Eet.Communication
{
    public class EetSoapClient
    {
        public EetSoapClient(Certificate certificate, EetEnvironment environment)
        {
            Environment = environment;
            var subdomain = environment == EetEnvironment.Production ? "prod" : "pg";
            var endpointUri = new Uri($"https://{subdomain}.eet.cz:443/eet/services/EETServiceSOAP/v3");
            SoapClient = new SoapClient(endpointUri, certificate.X509Certificate2);
        }

        public EetEnvironment Environment { get; }

        private SoapClient SoapClient { get; }

        public Task<SendRevenueXmlResponse> SendRevenueAsync(SendRevenueXmlMessage message)
        {
            return SoapClient.SendAsync<SendRevenueXmlMessage, SendRevenueXmlResponse>(message, operation: "http://fs.mfcr.cz/eet/OdeslaniTrzby");
        }
    }
}
