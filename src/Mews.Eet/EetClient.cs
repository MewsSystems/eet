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

        public Task<SendRevenueResult> SendRevenueAsync(RevenueRecord record, EetMode mode = EetMode.Operational)
        {
            var task = EetSoapClient.SendRevenueAsync(new SendRevenueMessage(record, mode).GetXmlMessage());
            return task.ContinueWith(t => Task.FromResult(new SendRevenueResult(t.Result))).Unwrap();
        }
    }
}
