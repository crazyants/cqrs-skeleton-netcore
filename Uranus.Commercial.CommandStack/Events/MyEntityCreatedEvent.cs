using System;
using Uranus.Commercial.Infrastructure.Framework;

namespace Uranus.Commercial.CommandStack.Events
{
    public class MyEntityCreatedEvent : IEvent
    {
        public MyEntityCreatedEvent()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.UtcNow;
        }

        #region [ IEvent Members ]

        public Guid Id { get; set; }

        public Guid AggregateId { get; set; }

        public DateTime CreatedDate { get; }

        #endregion

        public string PropertyOne { get; set; }

        public string PropertyTwo { get; set; }
    }
}
