using System;
using System.Threading.Tasks;
using Mews.Eet.Communication;
using Mews.Eet.Dto;

namespace Mews.Eet
{
    public class EetClient : IDisposable
    {
        private readonly EetSoapClient eetSoapClient;

        public EetClient(Certificate certificate, EetEnvironment environment = EetEnvironment.Production)
        {
            eetSoapClient = new EetSoapClient(certificate, environment);
        }

        public async Task<SendRevenueResult> SendRevenueAsync(RevenueRecord record, EetMode mode = EetMode.Operational)
        {
            var message = new SendRevenueMessage(record, mode).GetXmlMessage();
            var revenueResponse = await eetSoapClient.SendRevenueAsync(message).ConfigureAwait(false);

            return new SendRevenueResult(revenueResponse);
        }

        public void Dispose()
        {
            eetSoapClient.Dispose();
        }
    }
}
