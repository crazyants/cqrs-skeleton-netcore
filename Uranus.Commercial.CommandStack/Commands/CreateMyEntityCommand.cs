using System;
using Uranus.Commercial.Infrastructure.Framework;

namespace Uranus.Commercial.CommandStack.Commands
{
    public class CreateMyEntityCommand : ICommand
    {
        public CreateMyEntityCommand(string propertyOne, string propertyTwo)
        {
            PropertyOne = propertyOne;
            PropertyTwo = propertyTwo;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }

        public string PropertyOne { get; private set; }

        public string PropertyTwo { get; private set; }
    }
}
