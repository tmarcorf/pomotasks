using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomotasks.Domain.Interfaces
{
    public interface ITask : IEntityBase
    {
        public string Title { get; set; }

        public string Details { get; set; }

        public bool Done { get; set; }
    }
}
