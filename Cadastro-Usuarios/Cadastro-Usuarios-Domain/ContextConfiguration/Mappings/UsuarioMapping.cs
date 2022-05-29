using Cadastro_Usuarios_Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro_Usuarios_Domain.ContextConfiguration.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(c => c.Nome)
                .HasColumnType("varchar(100)");

            builder.Property(c => c.Sobrenome)
                .HasColumnType("varchar(100)");

            builder.Property(c => c.Email)
                .HasColumnType("varchar(100)");

            builder.Property(c => c.DataNascimento)
                .HasColumnType("DateTime");

            builder.ToTable("Usuarios");
        }
    }
}
