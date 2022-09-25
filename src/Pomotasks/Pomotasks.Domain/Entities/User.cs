using Pomotasks.Domain.Enums;
using Pomotasks.Domain.Interfaces;

namespace Pomotasks.Domain.Entities
{
    public class User : EntityBase, IUser
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public GenderTypeEnum GenderType { get; set; }

        public Address Address { get; set; }

        public List<Todo> Todos { get; set; }
    }
}
