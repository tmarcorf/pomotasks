using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomotasks.Domain.Dtos
{
    public class DtoTodo : DtoBase
    {
        public string Title { get; set; }

        public string Details { get; set; }

        public bool Done { get; set; }
    }
}
