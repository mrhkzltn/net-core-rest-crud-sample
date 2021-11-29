namespace NetCoreRestCrudSample.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        public string? Content { get; set; }

        public string? Language { get; set; }

        public int LibraryId { get; set; }


    }
}
