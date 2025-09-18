namespace Desafio_Desenvolvedor.Api.Contracts
{
    public class PostInfo
    {
        public Guid Postid { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime CreatedAtUtc { get; set; }
        public int CommentaryQuantity { get; set; } = 0;
    }
}
