using System.Security.Cryptography.X509Certificates;

namespace Mews.Eet.Providers
{
    public interface ICertificateProvider<in T>
        where T : class
    {
        X509Certificate2 GetCertificate(T data);
    }
}