namespace Pomotasks.Domain.Dtos
{
    public class DtoSingleResult<T> : DtoResult where T : class
    {
        public T? DtoResult { get; set; }
    }
}
