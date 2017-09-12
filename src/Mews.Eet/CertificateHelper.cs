using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Mews.Eet.Providers;

namespace Mews.Eet
{
    public class CertificateHelper
    {
        public virtual X509Certificate2 GetCertificate<T>(T data)
            where T : class
        {
            if (data is FileSystemCertificateParams)
            {
                return new FileSystemCertificateProvider().GetCertificate(data as FileSystemCertificateParams);
            }

            if (data is CertificatesStoreParams)
            {
                return new CertificatesStoreCertificateProvider().GetCertificate(data as CertificatesStoreParams);
            }

            return null;
        }

        public virtual AsymmetricAlgorithm GetKey<T>(T data)
            where T : class
        {
            if (data is X509Certificate2)
            {
                return new AsymmetricAlgorithmProvider().GetAsymmetricAlgorithm(data as X509Certificate2);
            }
            return null;
        }
    }
}