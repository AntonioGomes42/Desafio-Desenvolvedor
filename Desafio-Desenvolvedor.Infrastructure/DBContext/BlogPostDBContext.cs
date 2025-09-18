using Desafio_Desenvolvedor.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Desafio_Desenvolvedor.Infrastructure.DBContext
{
    public class BlogPostDBContext(DbContextOptions<BlogPostDBContext> options) : DbContext(options)
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                        .ToTable("TB_POSTS")
                        .HasMany(p => p.Comments)
                        .WithOne()
                        .HasForeignKey(c => c.PostId)
                        .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Comment>()
                        .ToTable("TB_POSTS_COMMENTS")
                        .HasKey(c => c.Id); 
        }
    }
}
