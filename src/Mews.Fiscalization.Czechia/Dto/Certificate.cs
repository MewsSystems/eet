using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Mews.Eet.Dto
{
    public class Certificate
    {
        public Certificate(string password, byte[] data, bool useMachineKeyStore = false)
        {
            Password = password;
            KeyStorageFlags = GetKeyStorageFlags(useMachineKeyStore);
            Data = data;
            Key = ComputeKey();
            X509Certificate2 = new X509Certificate2(Data, Password, KeyStorageFlags);
            UseMachineKeyStore = useMachineKeyStore;
        }

        public string Password { get; }

        public byte[] Data { get; }

        public bool UseMachineKeyStore { get; }

        public DSACryptoServiceProvider Key { get; }

        public X509Certificate2 X509Certificate2 { get; }

        private X509KeyStorageFlags KeyStorageFlags { get; }

        private X509KeyStorageFlags GetKeyStorageFlags(bool useMachineKeyStore)
        {
            if (useMachineKeyStore)
            {
                return X509KeyStorageFlags.Exportable | X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet;
            }

            return X509KeyStorageFlags.Exportable;
        }

        private DSACryptoServiceProvider ComputeKey()
        {
            var certificateCollection = new X509Certificate2Collection();
            certificateCollection.Import(Data, Password, KeyStorageFlags);
            foreach (var certificate in certificateCollection)
            {
                if (!certificate.HasPrivateKey)
                {
                    continue;
                }

                var key = certificate.PrivateKey as DSACryptoServiceProvider;
                var exportParameters = key.ExportParameters(includePrivateParameters: true);
                var cspParameters = new CspParameters
                {
                    ProviderName = "Microsoft Enhanced RSA and AES Cryptographic Provider",
                    Flags = UseMachineKeyStore ? CspProviderFlags.UseMachineKeyStore : CspProviderFlags.NoFlags
                };
                var result = new DSACryptoServiceProvider(cspParameters);
                result.ImportParameters(exportParameters);
                return result;
            }

            throw new ArgumentException("The provided certificate does not have any private keys.");
        }
    }
}
