using Desafio_Desenvolvedor.Domain.Entities;

namespace Desafio_Desenvolvedor.Application.Interfaces
{
    public interface IRepository : IDisposable
    {
        Task<List<Post>> GetAllPostsAsync(CancellationToken stoppingToken = default);
        Task CreatePostAsync(Post post, CancellationToken stoppingToken = default);
        Task<Post> GetPostByIdAsync(Guid id, CancellationToken stoppingToken = default);
        Task AddCommentToPostAsync(Guid postId, Comment comment, CancellationToken stoppingToken = default);
    }
}
