using Pomotasks.Domain.Entities;
using Pomotasks.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomotasks.Domain.Interfaces
{
    public interface IUser : IEntityBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public GenderTypeEnum GenderType { get; set; }

        public Address Address { get; set; }

        public List<Todo> Todos { get; set; }
    }
}
