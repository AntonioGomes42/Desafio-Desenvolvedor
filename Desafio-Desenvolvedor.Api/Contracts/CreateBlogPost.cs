using System.ComponentModel.DataAnnotations;

namespace Desafio_Desenvolvedor.Api.Contracts
{
    public class CreateBlogPost
    {
        public class CreateBlogPostRequest
        {
            [Required, MinLength(10), MaxLength(255)]
            public string? Title { get; set; }

            public string? Author { get; set; }

            [Required, MinLength(10), MaxLength(255)]
            public string? Content { get; set; }
        }

        public class CreateBlogPostResponse
        {
            public Guid Id { get; set; }
            public string? Title { get; set; }
            public DateTime CreatedAtUtc { get; set; }
        }
    }
}
