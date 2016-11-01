using System;

namespace Uranus.Commercial.Infrastructure.Framework
{
    public interface IEvent
    {
        Guid AggregateId { get; }

        DateTime CreatedDate { get; }
    }
}
