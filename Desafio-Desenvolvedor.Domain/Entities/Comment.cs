namespace Desafio_Desenvolvedor.Domain.Entities
{
    public class Comment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Author { get; set; }
        public required string Text { get; set; }
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public Guid PostId { get; set; }
    }
}
