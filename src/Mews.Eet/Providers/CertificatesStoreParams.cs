using System.Security.Cryptography.X509Certificates;

namespace Mews.Eet.Providers
{
    public class CertificatesStoreParams
    {
        public StoreLocation StoreLocation { get; set; }
        public StoreName StoreName { get; set; }
        public X509FindType FindType { get; set; }
        public object FindValue { get; set; }
    }
}