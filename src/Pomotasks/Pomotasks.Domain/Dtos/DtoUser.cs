namespace Pomotasks.Domain.Dtos
{
    public class DtoUser : DtoWithEmail
    {
        public DtoUser()
        {
            Todos = new List<DtoTodo>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int GenderType { get; set; }

        public DtoAddress Address { get; set; }

        public List<DtoTodo> Todos { get; set; }
    }
}
