using Cadastro_Usuarios_Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cadastro_Usuarios_Domain.ContextConfiguration
{
    public class UsuarioDbContext : DbContext
    {
        private readonly IMediator _mediatorHandler;
        public UsuarioDbContext(DbContextOptions<UsuarioDbContext> options, IMediator mediatorHandler) : base(options)
        {
            _mediatorHandler = mediatorHandler;
        }
        public DbSet<Usuario> Usuarios { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfigurationsFromAssembly(typeof(UsuarioDbContext).Assembly);

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;


            builder.Entity<Usuario>()
                .HasIndex(u => u.Id)
                .IsUnique(false);

            base.OnModelCreating(builder);
        }

        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }

            var sucesso = await base.SaveChangesAsync() > 0;

            return true;
        }
    }
}
