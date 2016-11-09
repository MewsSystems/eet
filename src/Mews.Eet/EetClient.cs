using System.Threading.Tasks;
using Mews.Eet.Communication;
using Mews.Eet.Dto;

namespace Mews.Eet
{
    public class EetClient
    {
        public EetClient(Certificate certificate, EetEnvironment environment = EetEnvironment.Production)
        {
            EetSoapClient = new EetSoapClient(certificate, environment);
        }

        private EetSoapClient EetSoapClient { get; }

        public SendRevenueResult SendRevenue(RevenueRecord record, EetMode mode = EetMode.Operational)
        {
            return SendRevenueAsync(record, mode).Result;
        }

        public async Task<SendRevenueResult> SendRevenueAsync(RevenueRecord record, EetMode mode = EetMode.Operational)
        {
            var message = new SendRevenueMessage(record, mode).GetXmlMessage();
            var revenueResponse = await EetSoapClient.SendRevenueAsync(message).ConfigureAwait(false);

            return new SendRevenueResult(revenueResponse);
        }
    }
}
