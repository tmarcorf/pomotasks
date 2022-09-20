using Pomotasks.Domain.Entities;
using Pomotasks.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomotasks.Domain.Dtos
{
    public class DtoUser : DtoWithEmail
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int GenderType { get; set; }

        public DtoAddress Address { get; set; }
    }
}
