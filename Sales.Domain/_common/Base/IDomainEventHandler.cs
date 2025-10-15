
using MediatR;

namespace Sales.Domain._common.Base
{
    public interface IDomainEventHandler<T> : INotificationHandler<T> where T : INotification
    {
    }
}
