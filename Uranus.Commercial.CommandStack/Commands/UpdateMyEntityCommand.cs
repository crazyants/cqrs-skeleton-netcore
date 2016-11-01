using System;
using Uranus.Commercial.Infrastructure.Framework;

namespace Uranus.Commercial.CommandStack.Commands
{
    public class UpdateMyEntityCommand : ICommand
    {
        public Guid Id { get; set; }

        public string PropertyOne { get; private set; }

        public string PropertyTwo { get; private set; }
    }
}
