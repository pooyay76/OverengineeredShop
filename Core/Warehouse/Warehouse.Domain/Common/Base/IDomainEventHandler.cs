
using MediatR;

namespace Warehouse.Domain.Common.Base
{
    public interface IDomainEventHandler<T> : INotificationHandler<T> where T : INotification
    {
    }
}
