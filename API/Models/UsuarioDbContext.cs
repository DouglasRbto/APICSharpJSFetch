using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class UsuarioDbContext(DbContextOptions<UsuarioDbContext> options) : DbContext(options)
    {
        public required DbSet<Usuario> Usuarios { get; set; }
    }
}
