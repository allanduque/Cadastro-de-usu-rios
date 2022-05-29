using Cadastro_Usuarios_Domain.DTOs;
using Cadastro_Usuarios_Domain.Entities;
using Cadastro_Usuarios_Domain.IntegrationTypes;
using FluentValidation;

namespace Cadastro_Usuarios_Application.Commands.CadastrarUsuario
{
    public class DeletarUsuarioCommand : Command<UsuarioResponse>
    {
        public UsuarioDTO usuarioDTO { get; set; }

        public DeletarUsuarioCommand(UsuarioDTO usuarioDTO)
        {
            this.usuarioDTO = usuarioDTO;
        }

        public DeletarUsuarioCommand(int id)
        {
            this.usuarioDTO = new UsuarioDTO();
            this.usuarioDTO.Id = id;
        }

        public override bool EhValido()
        {
            ValidationResult = new DeletarUsuarioValidation().Validate(this);
            return ValidationResult.IsValid;
        }
        public List<string> messageErro()
        {
            return ValidationResult.Errors.Select(c => c.ErrorMessage).ToList();
        }

        public Usuario ToEntity()
        {
            return new Usuario(usuarioDTO.Nome, usuarioDTO.Sobrenome, usuarioDTO.Email, DateTime.Parse(usuarioDTO.DataNascimento), usuarioDTO.Escolaridade);
        }
        public class DeletarUsuarioValidation : AbstractValidator<DeletarUsuarioCommand>
        {
            public DeletarUsuarioValidation()
            {
                RuleFor(c => c.usuarioDTO.Id)
                    .NotNull()
                    .NotEmpty()
                    .GreaterThan(0)
                    .WithMessage("Id do usuário é inválido");
            }
        }
    }
}
