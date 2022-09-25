using Pomotasks.Domain.Interfaces;

namespace Pomotasks.Domain.Entities
{
    public class Todo : EntityBase, ITodo
    {
        public string Title { get; set; }

        public string Details { get; set; }

        public DateTime CreationDate { get; set; }

        public bool Done { get; set; }

        public Guid UserId { get; set; }
    }
}
