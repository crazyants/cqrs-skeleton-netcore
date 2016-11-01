using System.Threading.Tasks;

namespace Uranus.Commercial.Infrastructure.Framework
{
    public interface ICommandHandler<in TCommand> where TCommand : class, ICommand
    {
        Task HandleAsync(TCommand command);

        void Validate(TCommand command);
    }
}
