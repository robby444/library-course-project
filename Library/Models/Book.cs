namespace Library.Models
{
    public abstract class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public int Pages { get; set; }
        public string? Description { get; set; }

        public ICollection<Review>? Reviews { get; set; }
    }
}
