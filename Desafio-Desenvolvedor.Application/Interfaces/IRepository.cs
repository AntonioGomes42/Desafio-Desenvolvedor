using Desafio_Desenvolvedor.Domain.Entities;

namespace Desafio_Desenvolvedor.Application.Interfaces
{
    public interface IRepository : IDisposable
    {
        Task<List<BlogPost>> GetAllPostsAsync(CancellationToken stoppingToken = default);
        Task CreatePostAsync(BlogPost post, CancellationToken stoppingToken = default);
        Task<BlogPost?> GetPostByIdAsync(Guid id, CancellationToken stoppingToken = default);
        Task AddCommentToPostAsync(Guid postId, Comment comment, CancellationToken stoppingToken = default);
    }
}
