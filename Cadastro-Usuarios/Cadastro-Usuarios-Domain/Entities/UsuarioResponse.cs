using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro_Usuarios_Domain.Entities
{
    public class UsuarioResponse
    {
        public Usuario Usuario { get; set; }
        public bool Sucesso { get; set; }

        public List<string>? Erros { get; set; }

        public UsuarioResponse(Usuario usuario, bool sucesso, List<string>? erros = null)
        {
            Usuario = usuario;
            Sucesso = sucesso;
            Erros = erros;
        }
    }
}
