namespace Desafio_Desenvolvedor.Api.Contracts
{
    public class GetBlogPostResponse
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public string? Content { get; set; }
        public List<CommentInfo> Comments { get; set; } = [];
    }

    public class CommentInfo
    {
        public Guid Id { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public string? Author { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}
