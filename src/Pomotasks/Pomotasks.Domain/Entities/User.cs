using Pomotasks.Domain.Enums;
using Pomotasks.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomotasks.Domain.Entities
{
    public class User : EntityBase, IUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public GenderTypeEnum GenderType { get; set; }

        public Address Address { get; set; }
    }
}
