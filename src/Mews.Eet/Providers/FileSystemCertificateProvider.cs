using System.Security.Cryptography.X509Certificates;

namespace Mews.Eet.Providers
{
    public class FileSystemCertificateProvider : ICertificateProvider<FileSystemCertificateParams>
    {
        public X509Certificate2 GetCertificate(FileSystemCertificateParams data)
        {
            return new X509Certificate2(fileName: data.FilePath, password: data.Password,
                keyStorageFlags: GetKeyStorageFlags(data.UseMachienceKeyStore));
        }

        private X509KeyStorageFlags GetKeyStorageFlags(bool useMachineKeyStore)
        {
            if (useMachineKeyStore)
            {
                return X509KeyStorageFlags.Exportable | X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet;
            }
            return X509KeyStorageFlags.Exportable;
        }
    }
}