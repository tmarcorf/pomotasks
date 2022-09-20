using Pomotasks.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomotasks.Domain.Entities
{
    public abstract class EntityBase : IEntityBase
    {
        public Guid Id { get; set; }
    }
}
