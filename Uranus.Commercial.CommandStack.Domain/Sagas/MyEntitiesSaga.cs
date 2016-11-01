using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uranus.Commercial.Infrastructure.Framework;

namespace Uranus.Commercial.CommandStack.Domain.Sagas
{
    public class MyEntitiesSaga : Saga
    {
        public MyEntitiesSaga(IEventBus eventBus, ICommandBus commandBus)
            : base(eventBus, commandBus)
        { }

        public override Guid Id { get; }

        public override long Version { get; }

        public override void Transition()
        {
            throw new NotImplementedException();
        }
    }
}
