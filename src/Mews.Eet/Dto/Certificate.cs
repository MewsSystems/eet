using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Mews.Eet.Dto
{
    public class Certificate
    {
        public Certificate(string password, byte[] data)
        {
            Password = password;
            Data = data;
            Key = ComputeKey();
            X509Certificate2 = new X509Certificate2(Data, Password, X509KeyStorageFlags.DefaultKeySet);
        }

        public string Password { get; }

        public byte[] Data { get; }

        public RSACryptoServiceProvider Key { get; }

        public X509Certificate2 X509Certificate2 { get; }

        private RSACryptoServiceProvider ComputeKey()
        {
            var certificateCollection = new X509Certificate2Collection();
            certificateCollection.Import(Data, Password, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable);
            foreach (var certificate in certificateCollection)
            {
                if (!certificate.HasPrivateKey)
                {
                    continue;
                }

                var key = certificate.PrivateKey as RSACryptoServiceProvider;
                var exportParameters = key.ExportParameters(includePrivateParameters: true);
                var cspParameters = new CspParameters { ProviderName = "Microsoft Enhanced RSA and AES Cryptographic Provider" };
                var result = new RSACryptoServiceProvider(cspParameters);
                result.ImportParameters(exportParameters);
                return result;
            }

            throw new ArgumentException("The provided certificate does not have any private keys.");
        }
    }
}
