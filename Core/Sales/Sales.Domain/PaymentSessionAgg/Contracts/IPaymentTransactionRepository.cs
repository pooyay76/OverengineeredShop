using Sales.Domain.PaymentSessionAgg.Models;

namespace Sales.Domain.PaymentSessionAgg.Contracts
{
    public interface IPaymentTransactionRepository
    {
        void Create(PaymentSession entity);
        void Update(PaymentSession entity);
        PaymentSession Get(long id);
        List<PaymentSession> GetOrders();
    }
}
