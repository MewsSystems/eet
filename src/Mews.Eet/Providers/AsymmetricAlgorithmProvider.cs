using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Mews.Eet.Providers
{
    public class AsymmetricAlgorithmProvider : IAsymmetricAlgorithmProvider<X509Certificate2>
    {
        public AsymmetricAlgorithm GetAsymmetricAlgorithm(X509Certificate2 x509Certificate2)
        {
            if (x509Certificate2.HasPrivateKey)
            {
                if (x509Certificate2.PrivateKey is RSACryptoServiceProvider)
                {
                    return x509Certificate2.GetRSAPrivateKey();
                }

                return x509Certificate2.PrivateKey;
            }
            throw new CryptographicException("Private Key Not Found");
        }
    }
}
