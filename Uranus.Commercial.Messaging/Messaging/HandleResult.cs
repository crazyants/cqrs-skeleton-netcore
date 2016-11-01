using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uranus.Commercial.Infrastructure.Framework;

namespace Uranus.Commercial.Messaging
{
    public class HandleResult : IHandleResult
    {
        public bool Succeed
        {
            get
            {
                return true;
            }
        }
    }
}
