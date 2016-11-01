using System;
using Uranus.Commercial.QueryStack.Common;

namespace Uranus.Commercial.QueryStack.Model
{
    public class MyEntityView : IProjectionView
    {
        public Guid Id { get; set; }

        public string PropertyOne { get; set; }

        public string ProperyTwo { get; set; }
    }
}
