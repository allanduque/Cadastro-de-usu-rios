using Cadastro_Usuarios_Domain.Data;
using Cadastro_Usuarios_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro_Usuarios_Domain.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario CadastrarUsuario(Usuario usuario);
        List<Usuario> BuscarTodosUsuarios();
        Usuario DeletarUsuario(Usuario usuario);
        Usuario AtualizarUsuario(Usuario usuario);
        Usuario? BuscarUsuarioPorId(int Id);
    }
}
