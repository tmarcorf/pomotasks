using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomotasks.Domain.Dtos
{
    public abstract class DtoWithEmail : DtoBase
    {
        public string Email { get; set; }
    }
}
