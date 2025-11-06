namespace Sales.Domain.OrderAgg.Models
{
    public enum OrderStatus
    {
        AwaitingConfirmation,
        Confirmed,
        Refunded,
        Sent,
        Delivered
    }
}
