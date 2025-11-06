
using MediatR;

namespace Sales.Domain.Common.Base
{
    public interface IDomainEventHandler<T> : INotificationHandler<T> where T : INotification
    {
    }
}
