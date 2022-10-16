namespace Pomotasks.Domain.Interfaces
{
    public interface ITodo : IEntityBase
    {
        string Title { get; set; }

        string Details { get; set; }

        DateTime CreationDate { get; set; }

        bool Done { get; set; }

        Guid UserId { get; set; }
    }
}
