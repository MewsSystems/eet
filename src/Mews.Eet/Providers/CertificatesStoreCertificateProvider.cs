using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Mews.Eet.Providers
{
    public class CertificatesStoreCertificateProvider : ICertificateProvider<CertificatesStoreParams>
    {
        public X509Certificate2 GetCertificate(CertificatesStoreParams data)
        {
            X509Store x509Store = new X509Store(data.StoreName, data.StoreLocation);
            x509Store.Open(OpenFlags.ReadOnly);
            var certificate2Collection =
                x509Store.Certificates.Find(data.FindType, data.FindValue, false)
                    .Cast<X509Certificate2>().ToList();
            X509Certificate2 signingCertificate = null;

            if (certificate2Collection.Count > 0)
            {
                signingCertificate =
                    certificate2Collection.FirstOrDefault(
                        certificate => certificate.NotAfter == certificate2Collection.Max(cert => cert.NotAfter));
            }
            if (signingCertificate != null && signingCertificate.HasPrivateKey)
            {
                return signingCertificate;
            }
            return null;
        }
    }
}