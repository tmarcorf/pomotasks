using Pomotasks.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomotasks.Domain.Interfaces
{
    public interface ITodo : IEntityBase
    {
       string Title { get; set; }

       string Details { get; set; }

       DateTime CreationDate { get; set; }

       bool Done { get; set; }

       Guid UserId { get; set; }

       User User { get; set; }
    }
}
