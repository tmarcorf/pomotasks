using Pomotasks.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomotasks.Domain.Entities
{
    public class Todo : EntityBase, ITodo
    {
        public string Title { get; set; }

        public string Details { get; set; }

        public DateTime CreationDate { get; set; }

        public bool Done { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
