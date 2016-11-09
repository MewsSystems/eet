using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mews.Eet.Communication
{
    public class SoapHttpClient
    {
        public SoapHttpClient(Uri endpointUri)
        {
            EndpointUri = endpointUri;
            HttpClient = new HttpClient();
        }

        private Uri EndpointUri { get; }

        private HttpClient HttpClient { get; }

        public async Task<string> SendAsync(string body, string operation)
        {
            HttpClient.DefaultRequestHeaders.Remove("SOAPAction");
            HttpClient.DefaultRequestHeaders.Add("SOAPAction", operation);

            using (var postResponse = await HttpClient.PostAsync(
                EndpointUri,
                new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded")).ConfigureAwait(false))
            {
                return await postResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
        }
    }
}