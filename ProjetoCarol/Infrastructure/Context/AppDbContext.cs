using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjetoCarol.Domain.Entities.Arquivo;
using ProjetoCarol.Domain.Entities.Usuario;
namespace ProjetoCarol.Infrastructure.Context;

public class AppDbContext : IdentityDbContext<Usuario, IdentityRole<Guid>, Guid>
{
    public DbSet<UsuarioAula> UsuarioAula { get; set; }
    public DbSet<UsuarioPagamento> UsuarioPagamento { get; set; }
    public DbSet<UsuarioMatricula> UsuarioMatricula { get; set; }
    public DbSet<Arquivo> Arquivos { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
