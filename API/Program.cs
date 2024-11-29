using Microsoft.EntityFrameworkCore;
using API.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<UsuarioDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("PermitirTudo");

app.MapGet("/usuarios", async (UsuarioDbContext context) =>
{
    return await context.Usuarios.ToListAsync();
});

app.MapPost("/usuarios", async (Usuario usuario, UsuarioDbContext context) =>
{
    context.Usuarios.Add(usuario);
    await context.SaveChangesAsync();
    return Results.Ok($"O usuário {usuario.Nome} foi adicionado com sucesso.");
});

app.MapDelete("/usuarios/{id:int}", async (int id, UsuarioDbContext context) =>
{
    var usuario = await context.Usuarios.FindAsync(id);
    
    if(usuario == null)
    {
        return Results.NotFound($"Usuário não encontrado.");
    }

    context.Usuarios.Remove(usuario);
    await context.SaveChangesAsync();
    return Results.Ok($"Usuário com ID {id} foi deletado com sucesso.");
});

app.Run();