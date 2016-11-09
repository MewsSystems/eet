using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mews.Eet.Communication
{
    public sealed class SoapHttpClient : IDisposable
    {
        private readonly Uri endpointUri;
        private readonly HttpClient httpClient;

        public SoapHttpClient(Uri endpointUri)
        {
            this.endpointUri = endpointUri;
            httpClient = new HttpClient();
        }

        public async Task<string> SendAsync(string body, string operation)
        {
            httpClient.DefaultRequestHeaders.Remove("SOAPAction");
            httpClient.DefaultRequestHeaders.Add("SOAPAction", operation);

            using (var postResponse = await httpClient.PostAsync(
                endpointUri,
                new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded")).ConfigureAwait(false))
            {
                return await postResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
        }

        public void Dispose()
        {
            httpClient.Dispose();
        }
    }
}