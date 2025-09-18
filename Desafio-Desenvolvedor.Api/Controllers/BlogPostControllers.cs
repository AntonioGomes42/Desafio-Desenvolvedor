using Desafio_Desenvolvedor.Api.Contracts;
using Desafio_Desenvolvedor.Application.Interfaces;
using Desafio_Desenvolvedor.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using static Desafio_Desenvolvedor.Api.Contracts.CreateBlogPost;

namespace Desafio_Desenvolvedor.Api.Controllers
{
    public static class BlogPostControllers
    {
        public static void MapBlogPostControllers(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/posts", async (
                CreateBlogPostRequest request,
                IRepository db,
                CancellationToken ct) =>
            {
                var validationContext = new ValidationContext(request);
                var validationResults = new List<ValidationResult>();
                bool isValid = Validator.TryValidateObject(request, validationContext, validationResults, true);

                if (!isValid)
                {
                    return Results.BadRequest(validationResults.Select(r => r.ErrorMessage));
                }

                var post = new Post
                {
                    Title = request.Title!,
                    Author = request.Author ?? "Anonymous",
                    Content = request.Content!
                };

                await db.CreatePostAsync(post, ct);

                var response = new CreateBlogPostResponse()
                {
                    Id = post.Id,
                    Title = post.Title,
                    CreatedAtUtc = post.CreatedAtUtc
                };

                return Results.Ok(response);
            });


            app.MapGet("/api/posts", async (
                IRepository db,
                CancellationToken ct) =>
            {
                List<Post> posts = await db.GetAllPostsAsync(ct);

                var response = posts.Select(post => new PostInfo() { Postid = post.Id, CommentaryQuantity = post.Comments.Count, Title = post.Title, CreatedAtUtc=post.CreatedAtUtc })
                                    .ToList();

                return Results.Ok(response);
            });

            app.MapGet("/api/posts/{Id}", async (
                Guid Id,
                IRepository db,
                CancellationToken ct) =>
            {
                try
                {
                    Post post = await db.GetPostByIdAsync(Id, ct);

                    GetBlogPostResponse postResponse = new()
                    {
                        Id = post.Id,
                        Title = post.Title,
                        Author = post.Author,
                        CreatedAtUtc = post.CreatedAtUtc,
                        Content = post.Content,
                        Comments = post.Comments.Select(c => new CommentInfo
                        {
                            Id = c.Id,
                            CreatedAtUtc = c.CreatedAtUtc,
                            Author = c.Author,
                            Text = c.Text
                        }).ToList()
                    };

                    return Results.Ok(postResponse);
                }
                catch (KeyNotFoundException)
                {
                    return Results.NotFound(Id);
                }
            });

            app.MapPost("/api/posts/{Id}/comments", async (
                Guid Id,
                CreateCommentRequest request,
                IRepository db,
                CancellationToken ct) =>
            {
                var validationContext = new ValidationContext(request);
                var validationResults = new List<ValidationResult>();
                bool isValid = Validator.TryValidateObject(request, validationContext, validationResults, true);

                if (!isValid)
                {
                    return Results.BadRequest(validationResults.Select(r => r.ErrorMessage));
                }

                var comment = new Comment
                {
                    Author = request.Author ?? "Anonymous",
                    Text = request.Text
                };

                try
                {
                    await db.AddCommentToPostAsync(Id, comment, ct);
                    return Results.Ok();
                }
                catch (KeyNotFoundException)
                {
                    return Results.NotFound(Id);
                }
            });

        }
    }
}
