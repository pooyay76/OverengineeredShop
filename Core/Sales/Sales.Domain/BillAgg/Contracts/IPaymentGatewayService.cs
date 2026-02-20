using Common.Domain.Language.Global.ValueObjects;
using Common.Domain.Language.Sales.ValueObjects;

namespace Sales.Domain.BillAgg.Contracts
{
    public interface IPaymentGatewayService
    {
        Task<string> GetNewSessionIdAsync(Money amount, BillId billId);
        string GetPaymentPageUrl(string sessionId);
        Task<bool> VerifyPaymentAsync(string sessionId, Money amount);
    }
}
