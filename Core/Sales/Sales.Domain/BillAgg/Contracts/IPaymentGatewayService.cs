using Sales.Domain.BillAgg.Models;
using Sales.Domain.Common.ValueObjects;

namespace Sales.Domain.BillAgg.Contracts
{
    public interface IPaymentGatewayService
    {
        Task<string> GetNewSessionIdAsync(Money amount, BillId billId);
        string GetPaymentPageUrl(string sessionId);
        Task<bool> VerifyPaymentAsync(string sessionId, Money amount);
    }
}
