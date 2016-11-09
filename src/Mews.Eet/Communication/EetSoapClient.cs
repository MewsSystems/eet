using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Mews.Eet.Dto;
using Mews.Eet.Dto.Wsdl;

namespace Mews.Eet.Communication
{
    public sealed class EetSoapClient : IDisposable
    {
        private readonly EetEnvironment environment;
        private readonly X509Certificate2 x509certificate;
        private readonly SoapClient soapClient;

        public EetSoapClient(Certificate certificate, EetEnvironment environment)
        {
            this.environment = environment;
            var subdomain = environment == EetEnvironment.Production ? "prod" : "pg";
            var endpointUri = new Uri($"https://{subdomain}.eet.cz:443/eet/services/EETServiceSOAP/v3");
            x509certificate = new X509Certificate2(certificate.Data, certificate.Password);
            soapClient = new SoapClient(endpointUri, x509certificate);
        }

        public Task<SendRevenueXmlResponse> SendRevenueAsync(SendRevenueXmlMessage message)
        {
            return soapClient.SendAsync<SendRevenueXmlMessage, SendRevenueXmlResponse>(message, operation: "http://fs.mfcr.cz/eet/OdeslaniTrzby");
        }

        public void Dispose()
        {
            soapClient.Dispose();
            x509certificate.Dispose();
        }
    }
}
