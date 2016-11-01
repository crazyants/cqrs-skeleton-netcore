using System;

namespace Uranus.Commercial.Infrastructure.Framework
{
    public abstract class Saga
    {
        protected IEventBus _eventBus;
        protected ICommandBus _commandBus;

        public abstract Guid Id { get; }

        public abstract long Version { get; }

        public Saga(IEventBus eventBus, ICommandBus commandBus)
        {
            _eventBus = eventBus;
            _commandBus = commandBus;
        }

        public abstract void Transition();
    }
}
