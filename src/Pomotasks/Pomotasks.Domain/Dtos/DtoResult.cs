namespace Pomotasks.Domain.Dtos
{
    public class DtoResult
    {
        public DtoResult()
        {
            Errors = new List<string>();
        }

        public List<string> Errors { get; set; }

        public bool Success { get; set; }
    }
}
