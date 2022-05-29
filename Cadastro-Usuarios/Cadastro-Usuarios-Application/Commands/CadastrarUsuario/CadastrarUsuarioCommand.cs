using Cadastro_Usuarios_Domain.DTOs;
using Cadastro_Usuarios_Domain.Entities;
using Cadastro_Usuarios_Domain.IntegrationTypes;
using FluentValidation;

namespace Cadastro_Usuarios_Application.Commands.CadastrarUsuario
{
    public class CadastrarUsuarioCommand : Command<Usuario>
    {
        public UsuarioDTO usuarioDTO { get; set; }

        public CadastrarUsuarioCommand(UsuarioDTO usuarioDTO)
        {
            this.usuarioDTO = usuarioDTO;
        }

        public override bool EhValido()
        {
            ValidationResult = new CadastrarUsuarioValidation().Validate(this);
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
        public class CadastrarUsuarioValidation : AbstractValidator<CadastrarUsuarioCommand>
        {
            public CadastrarUsuarioValidation()
            {
                RuleFor(c => c.usuarioDTO.Nome)
                    .NotNull()
                    .NotEmpty()
                    .MinimumLength(3)
                    .WithMessage("Nome do usuário é inválido");

                RuleFor(c => c.usuarioDTO.Sobrenome)
                    .NotNull()
                    .NotEmpty()
                    .MinimumLength(3)
                    .WithMessage("Sobrenome do usuário é inválido");

                RuleFor(c => c.usuarioDTO.Email)
                    .NotNull()
                    .NotEmpty()
                    .MinimumLength(3)
                    .EmailAddress()
                    .WithMessage("Email do usuário é inválido");

                RuleFor(c => c.usuarioDTO.DataNascimento)
                    .NotNull()
                    .NotEmpty()
                    .MinimumLength(8)
                    .WithMessage("Data de Nascimento do usuário é inválido");

                RuleFor(c => c.usuarioDTO.Escolaridade)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("Escolaridade do usuário é inválido");
            }
        }
    }
}
