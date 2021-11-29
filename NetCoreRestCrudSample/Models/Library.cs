namespace NetCoreRestCrudSample.Models
{
    public class Library
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public ICollection<Book>? Books { get; set; }
    }
}
