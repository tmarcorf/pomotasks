using Pomotasks.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomotasks.Domain.Entities
{
    public class UserLogin : User, IUserLogin
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
