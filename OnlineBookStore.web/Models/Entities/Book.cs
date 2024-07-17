namespace OnlineBookStore.web.Models.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
    }
}
