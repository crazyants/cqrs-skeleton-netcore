using System;
using Uranus.Commercial.CommandStack.Domain.Common;

namespace Uranus.Commercial.CommandStack.Domain.Model
{
    public class MyEntity : AggregateRoot       
    {
        public MyEntity()
        {
            this.Id = Guid.NewGuid();
        }      

        public string PropertyOne { get; set; }

        public string PropertyTwo { get; set; }
    }
}
