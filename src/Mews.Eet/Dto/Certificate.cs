using Mews.Eet.Extensions;
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

        public RSACryptoServiceProvider Key { get; }

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

        private RSACryptoServiceProvider ComputeKey()
        {
            var certificateCollection = new X509Certificate2Collection();
            certificateCollection.Import(Data, Password, KeyStorageFlags);
            foreach (var certificate in certificateCollection)
            {
                if (!certificate.HasPrivateKey)
                {
                    continue;
                }

				return certificate.GetPrivateKeyRsaCryptoServiceProvider(UseMachineKeyStore);
            }

            throw new ArgumentException("The provided certificate does not have any private keys.");
        }
    }
}
