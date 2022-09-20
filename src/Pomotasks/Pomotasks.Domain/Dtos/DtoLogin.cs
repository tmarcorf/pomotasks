using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomotasks.Domain.Dtos
{
    public class DtoLogin : DtoWithEmail
    {
        public string Password { get; set; }
    }
}
