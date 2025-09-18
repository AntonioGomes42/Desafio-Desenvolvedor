namespace Desafio_Desenvolvedor.Domain.Entities
{
    public class Post
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public ICollection<Comment> Comments { get; set; } = [];
    }
}
