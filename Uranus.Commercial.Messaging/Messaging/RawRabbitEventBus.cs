using RawRabbit;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Uranus.Commercial.Infrastructure.Framework;

namespace Uranus.Commercial.Messaging
{
    public class RawRabbitEventBus : IEventBus
    {
        protected readonly IBusClient _busClient;
        protected readonly IServiceProvider _serviceProvider;
        protected readonly IDictionary<Type, IList<Type>> _eventHandlers = new Dictionary<Type, IList<Type>>();

        public RawRabbitEventBus(IBusClient busClient, IServiceProvider serviceProvider)
        {
            _busClient = busClient;
            _serviceProvider = serviceProvider;

            _busClient.RespondAsync<IEvent, IHandleResult>(async (@event, context) => 
            {
                var eventHandlerTypes = _eventHandlers[@event.GetType()];

                Parallel.ForEach(eventHandlerTypes, async (eventHandlerType) =>
                {
                    var eventHandlerInstance = _serviceProvider.GetService(eventHandlerType);

                    await (Task)eventHandlerInstance.GetType()
                                                .GetMethod("HandleAsync", new Type[] { @event.GetType() })
                                                .Invoke(eventHandlerInstance, new object[] { @event });
                });

                return await Task.FromResult(new HandleResult());
            });
        }

        #region [ IEventBus Members ]

        Task IEventBus.SendAsync<TEvent>(TEvent @event)
            => this._busClient.PublishAsync<IEvent>(@event);

        public async Task RegisterHandler(Type eventType, Type eventHandler)
        {
            var eventHandlers = (IList<Type>)null;

            if (!_eventHandlers.TryGetValue(eventType, out eventHandlers))
            {
                eventHandlers = new List<Type>();
                _eventHandlers.Add(eventType, eventHandlers);
            }

            eventHandlers.Add(eventHandler);

            await Task.CompletedTask;
        }

        #endregion
    }
}
