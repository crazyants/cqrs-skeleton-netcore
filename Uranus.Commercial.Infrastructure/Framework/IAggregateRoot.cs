using System;
using System.Collections.Generic;

namespace Uranus.Commercial.Infrastructure.Framework
{
    public interface IAggregateRoot
    {
        Guid Id { get; }

        int Version { get; }
    }
}
