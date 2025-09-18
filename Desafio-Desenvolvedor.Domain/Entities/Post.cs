namespace Desafio_Desenvolvedor.Domain.Entities
{
    public class Post
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Title { get; set; }
        public string Author { get; set; } = "Anonymous";
        public required string Content { get; set; }
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public ICollection<Comment> Comments { get; set; } = [];
    }
}
