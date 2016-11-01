using System;

namespace Uranus.Commercial.Infrastructure.Framework
{
    public interface ICommand
    {
        Guid Id { get; }
    }
}
