using System.Security.Cryptography;

namespace Mews.Eet.Providers
{
    public interface IAsymmetricAlgorithmProvider<in T>
        where T : class
    {
        AsymmetricAlgorithm GetAsymmetricAlgorithm(T data);
    }
}