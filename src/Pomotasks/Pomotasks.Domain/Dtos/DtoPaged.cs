namespace Pomotasks.Domain.Dtos
{
    public class DtoPaged<T> where T : class
    {
        public int CurrentPage { get; set; }

        public int Skip { get; set; }

        public int Take { get; set; }

        public int TotalCount { get; set; }

        public IEnumerable<T>? Data { get; set; }
    }
}
