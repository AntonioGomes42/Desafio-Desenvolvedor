using Desafio_Desenvolvedor.Application.Interfaces;
using Desafio_Desenvolvedor.Domain.Entities;
using Desafio_Desenvolvedor.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Desafio_Desenvolvedor.Infrastructure.Repository
{
    public class BlogPostRepository : IRepository
    {
        private readonly BlogPostDBContext _context;

        public BlogPostRepository(BlogPostDBContext context)
        {
            _context = context;
        }

        public async Task AddCommentToPostAsync(Guid postId, Comment comment, CancellationToken stoppingToken = default)
        {
            bool exists = await _context.Posts.AnyAsync(p => p.Id == postId, stoppingToken);

            if (!exists) throw new KeyNotFoundException($"O post com ID '{postId}' não foi encontrado.");

            comment.PostId = postId;
            await _context.Comments.AddAsync(comment, stoppingToken);

            await _context.SaveChangesAsync(stoppingToken);
        }

        public async Task CreatePostAsync(Post post, CancellationToken stoppingToken = default)
        {
            await _context.Posts.AddAsync(post, stoppingToken);

            await _context.SaveChangesAsync(stoppingToken);
        }

        public async Task<List<Post>> GetAllPostsAsync(CancellationToken stoppingToken = default)
        {
            return await _context.Posts
                                 .AsNoTracking()
                                 .Include(p => p.Comments)
                                 .ToListAsync(stoppingToken);
        }

        public async Task<Post> GetPostByIdAsync(Guid id, CancellationToken stoppingToken = default)
        {
            Post? post = await _context.Posts
                                 .AsNoTracking()
                                 .Include(p => p.Comments)
                                 .SingleOrDefaultAsync(p => p.Id == id, stoppingToken);

            if (post is null) throw new KeyNotFoundException($"O post com ID '{id}' não foi encontrado.");

            return post;
        }

        public void EnsureCreated()
        {
            // Certifica que o banco de dados será criado, mesmo que o container do PostgreSQL ainda não esteja pronto para receber conexões.
            while (true)
            {
                try
                {
                    _context.Database.EnsureCreated();
                    break;
                }
                catch (Npgsql.NpgsqlException)
                {
                    Thread.Sleep(2 * 1000);
                }
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
