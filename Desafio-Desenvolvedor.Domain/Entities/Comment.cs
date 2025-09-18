namespace Desafio_Desenvolvedor.Domain.Entities
{
    public class Comment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Author { get; set; } = "Anonymous";
        public required string Text { get; set; }
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public Guid PostId { get; set; }
    }
}
