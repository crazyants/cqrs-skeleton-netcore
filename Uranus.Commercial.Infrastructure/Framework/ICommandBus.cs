using System;
using System.Threading.Tasks;

namespace Uranus.Commercial.Infrastructure.Framework
{
    public interface ICommandBus
    {
        Task RegisterHandler(Type commandType, Type commandHandler);

        Task<IHandleResult> SendAsync<TCommand>(TCommand command) where TCommand : class, ICommand;
    }
}
