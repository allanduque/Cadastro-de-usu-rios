using Cadastro_Usuarios_Domain.ContextConfiguration;
using Cadastro_Usuarios_Domain.Data;
using Cadastro_Usuarios_Domain.Entities;
using Cadastro_Usuarios_Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro_Usuarios_Infra.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UsuarioDbContext _context;

        public UsuarioRepository(UsuarioDbContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public IUnitOfWork UnitOfWork => _context;

        public Usuario AtualizarUsuario(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            return usuario;
        }

        public List<Usuario> BuscarTodosUsuarios()
        {
            return _context.Usuarios.ToList();
        }

        public Usuario? BuscarUsuarioPorId(int Id)
        {
            return _context.Usuarios.Where(s => s.Id == Id).FirstOrDefault();
        }

        public Usuario CadastrarUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            return usuario;
        }

        public Usuario DeletarUsuario(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
            return usuario;
        }

        public void Dispose()
        {
            _context.DisposeAsync();
        }
    }
}
