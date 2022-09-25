using Pomotasks.Domain.Entities;
using Pomotasks.Domain.Enums;

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
