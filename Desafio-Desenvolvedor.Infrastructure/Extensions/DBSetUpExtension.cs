using Desafio_Desenvolvedor.Application.Interfaces;
using Desafio_Desenvolvedor.Infrastructure.DBContext;
using Desafio_Desenvolvedor.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Desafio_Desenvolvedor.Infrastructure.Extensions
{
    public static class DBSetUpExtension
    {
        public static void RegisterDataBase(this IServiceCollection Services, string ConnectionString)
        {
            Services.AddDbContext<BlogPostDBContext>(
                        options => options.UseNpgsql(
                            ConnectionString,
                            npgsqlOptions => npgsqlOptions.EnableRetryOnFailure(
                                maxRetryCount: 3,
                                maxRetryDelay: TimeSpan.FromSeconds(5),
                                errorCodesToAdd: null
                            )
                        )
                    );

            Services.AddScoped<IRepository, BlogPostRepository>();
        }
    }
}
