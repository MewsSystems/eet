namespace Mews.Eet.Providers
{
    public class FileSystemCertificateParams
    {
        public string FilePath { get; set; }
        public string Password { get; set; }
        public bool UseMachienceKeyStore { get; set; } = false;
    }
}