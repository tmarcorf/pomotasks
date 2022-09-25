using Pomotasks.Domain.Interfaces;

namespace Pomotasks.Domain.Entities
{
    public abstract class EntityBase : IEntityBase
    {
        public Guid Id { get; set; }
    }
}
