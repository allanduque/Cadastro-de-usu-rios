using Cadastro_Usuarios_Domain.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cadastro_Usuarios_Domain.Entities
{
    public class Usuario : IAggregateRoot
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public int Escolaridade { get; set; }

        public Usuario(string nome, string sobrenome, string email, DateTime dataNascimento, int escolaridade)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Email = email;
            DataNascimento = dataNascimento;
            Escolaridade = escolaridade;
        }

        public Usuario(int id, string nome, string sobrenome, string email, DateTime dataNascimento, int escolaridade)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            Email = email;
            DataNascimento = dataNascimento;
            Escolaridade = escolaridade;
        }
    }
}
