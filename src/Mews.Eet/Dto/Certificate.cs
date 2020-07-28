using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Mews.Eet.Dto
{
    public class Certificate
    {
        public Certificate(string password, byte[] data)
        {
            X509Certificate2 = new X509Certificate2(data, password, X509KeyStorageFlags.DefaultKeySet);
            PrivateKey = X509Certificate2.GetRSAPrivateKey() ?? throw new ArgumentException("The provided certificate does not have an RSA key.");
        }

        public RSA PrivateKey { get; }

        public X509Certificate2 X509Certificate2 { get; }
    }
}
