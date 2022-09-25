namespace Pomotasks.Domain.Dtos
{
    public class DtoTodo : DtoBase
    {
        public string Title { get; set; }

        public string Details { get; set; }

        public DateTime CreationDate { get; set; }

        public bool Done { get; set; }

        public string UserId { get; set; }
    }
}
