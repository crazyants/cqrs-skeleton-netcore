using System;
using System.Threading.Tasks;
using Uranus.Commercial.CommandStack.Commands;
using Uranus.Commercial.CommandStack.Domain.Model;
using Uranus.Commercial.CommandStack.Domain.Repository;
using Uranus.Commercial.CommandStack.Events;
using Uranus.Commercial.Infrastructure.Data;
using Uranus.Commercial.Infrastructure.Framework;

namespace Uranus.Commercial.CommandStack.Domain.CommandHandlers
{
    public class MyEntityCommandHandler : 
        ICommandHandler<CreateMyEntityCommand>,
        ICommandHandler<UpdateMyEntityCommand>
    {
        private IUnitOfWork _unitOfWork;
        private IEventBus _eventBus;

        public MyEntityCommandHandler(IUnitOfWork unitOfWork, IEventBus eventBus)
        {
            _unitOfWork = unitOfWork;
            _eventBus = eventBus;
        }

        #region [ ICommandHandler<UpdateMyEntityCommand> Members ]

        public Task HandleAsync(UpdateMyEntityCommand command)
        {
            throw new NotImplementedException();
        }

        public void Validate(UpdateMyEntityCommand command)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region [ ICommandHandler<CreateMyEntityCommand> Members ]

        public async Task HandleAsync(CreateMyEntityCommand command)
        {
            Validate(command);

            var myEntity = new MyEntity
            {
                PropertyOne = command.PropertyOne,
                PropertyTwo = command.PropertyTwo
            };

            var myEntityRepository = _unitOfWork.GetRepository<IMyEntityRepository>();
            myEntityRepository.Save(myEntity);

            _unitOfWork.Commit();

            await _eventBus.SendAsync(new MyEntityCreatedEvent()
            {
                AggregateId = myEntity.Id,
                PropertyOne = myEntity.PropertyOne,
                PropertyTwo = myEntity.PropertyTwo
            });

            await Task.CompletedTask;
        }

        public void Validate(CreateMyEntityCommand command)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            if (command.Id == Guid.Empty || string.IsNullOrEmpty(command.PropertyOne) || string.IsNullOrEmpty(command.PropertyTwo))            
                throw new Exception("Invalid Command");            
        }

        #endregion
    }
}
