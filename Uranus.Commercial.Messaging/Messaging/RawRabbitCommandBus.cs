using RawRabbit;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Uranus.Commercial.Infrastructure.Framework;

namespace Uranus.Commercial.Messaging
{
    public class RawRabbitCommandBus : ICommandBus
    {
        protected readonly IBusClient _busClient;
        protected readonly IServiceProvider _serviceProvider;
        protected readonly IDictionary<Type, Type> _commandHandlers = new Dictionary<Type, Type>();

        public RawRabbitCommandBus(IBusClient busClient, IServiceProvider serviceProvider)
        {
            _busClient = busClient;
            _serviceProvider = serviceProvider;

            _busClient.RespondAsync<ICommand, IHandleResult>(async (command, context) =>
            {
                var commandHandlerType = _commandHandlers[command.GetType()];
                var commandHandlerInstance = _serviceProvider.GetService(commandHandlerType);

                await (Task)commandHandlerInstance.GetType()
                                                  .GetMethod("HandleAsync", new Type[] { command.GetType() })
                                                  .Invoke(commandHandlerInstance, new object[] { command });

                return new HandleResult();
            });
        }


        #region [ ICommand Interface Members ]

        Task<IHandleResult> ICommandBus.SendAsync<TCommand>(TCommand command) 
            => this._busClient.RequestAsync<ICommand, IHandleResult>(command);

        public async Task RegisterHandler(Type commandType, Type commandHandler)
        {
            _commandHandlers.Add(commandType, commandHandler);

            await Task.CompletedTask;
        }

        #endregion
    }
}
