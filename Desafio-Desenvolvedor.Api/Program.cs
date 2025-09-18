using Desafio_Desenvolvedor.Api.Controllers;
using Desafio_Desenvolvedor.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

string ConnectionString = builder.Configuration.GetConnectionString("Database") ?? throw new InvalidOperationException($"A propriedade 'Database' é nulo");
builder.Services.RegisterDataBase(ConnectionString);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapBlogPostControllers();

app.Run();