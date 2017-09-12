using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Mews.Eet.Dto
{
    public class Certificate
    {
        public Certificate(X509Certificate2 x509Certificate, AsymmetricAlgorithm key)
        {
            Key = key;
            X509Certificate2 = x509Certificate;
        }

        public AsymmetricAlgorithm Key { get; }

        public X509Certificate2 X509Certificate2 { get; }
    }
}
