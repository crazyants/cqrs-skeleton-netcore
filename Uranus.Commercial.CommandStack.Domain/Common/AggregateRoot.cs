using System;
using System.Collections.Generic;
using System.Reflection;
using Uranus.Commercial.Infrastructure.Framework;

namespace Uranus.Commercial.CommandStack.Domain.Common
{
    public abstract class AggregateRoot : IAggregateRoot
    {
        public Guid Id { get; protected set; }

        public int Version { get; protected set; } = -1;
    }
}
