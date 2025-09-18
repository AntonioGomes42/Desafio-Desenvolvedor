using System.ComponentModel.DataAnnotations;

namespace Desafio_Desenvolvedor.Api.Contracts
{
    public class CreateCommentRequest
    {
        public string? Author { get; set; }
        [Required, MinLength(10), MaxLength(255)]
        public string Text { get; set; } = string.Empty;
    }
}
