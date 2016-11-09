using System;
using System.Threading.Tasks;
using Mews.Eet.Communication;
using Mews.Eet.Dto;

namespace Mews.Eet
{
    public class EetClient : IDisposable
    {
        public EetClient(Certificate certificate, EetEnvironment environment = EetEnvironment.Production)
        {
            EetSoapClient = new EetSoapClient(certificate, environment);
        }

        private EetSoapClient EetSoapClient { get; }

        public async Task<SendRevenueResult> SendRevenueAsync(RevenueRecord record, EetMode mode = EetMode.Operational)
        {
            var message = new SendRevenueMessage(record, mode).GetXmlMessage();
            var revenueResponse = await EetSoapClient.SendRevenueAsync(message).ConfigureAwait(false);

            return new SendRevenueResult(revenueResponse);
        }

        public void Dispose()
        {
            EetSoapClient.Dispose();
        }
    }
}
