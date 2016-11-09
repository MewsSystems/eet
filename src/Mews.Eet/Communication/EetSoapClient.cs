using System;
using System.Threading.Tasks;
using Mews.Eet.Dto;
using Mews.Eet.Dto.Wsdl;

namespace Mews.Eet.Communication
{
    public sealed class EetSoapClient : IDisposable
    {
        private readonly EetEnvironment environment;
        private readonly SoapClient soapClient;

        public EetSoapClient(Certificate certificate, EetEnvironment environment)
        {
            this.environment = environment;
            var subdomain = environment == EetEnvironment.Production ? "prod" : "pg";
            var endpointUri = new Uri($"https://{subdomain}.eet.cz:443/eet/services/EETServiceSOAP/v3");
            soapClient = new SoapClient(endpointUri, certificate.X509Certificate2);
        }

        public Task<SendRevenueXmlResponse> SendRevenueAsync(SendRevenueXmlMessage message)
        {
            return soapClient.SendAsync<SendRevenueXmlMessage, SendRevenueXmlResponse>(message, operation: "http://fs.mfcr.cz/eet/OdeslaniTrzby");
        }

        public void Dispose()
        {
            soapClient.Dispose();
        }
    }
}
