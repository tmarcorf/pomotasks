using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomotasks.Domain.Interfaces
{
    internal interface IUserLogin
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
