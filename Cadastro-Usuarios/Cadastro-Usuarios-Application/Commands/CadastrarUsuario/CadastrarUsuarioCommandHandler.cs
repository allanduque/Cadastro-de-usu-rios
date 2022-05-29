using Cadastro_Usuarios_Domain.Entities;
using Cadastro_Usuarios_Domain.Interfaces;
using MediatR;

namespace Cadastro_Usuarios_Application.Commands.CadastrarUsuario
{
    public class CadastrarUsuarioCommandHandler : IRequestHandler<CadastrarUsuarioCommand, UsuarioResponse>
    {
        private readonly IMediator _mediator;
        private readonly IUsuarioRepository _usuarioRepository;

        private CancellationToken _cancellationToken;

        public CadastrarUsuarioCommandHandler(IUsuarioRepository usuarioRepository,
                                              IMediator mediator)
        {
            _mediator = mediator;
            _usuarioRepository = usuarioRepository;
        }
        public async Task<UsuarioResponse> Handle(CadastrarUsuarioCommand request, CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;

            var validaCommand = ValidarComando(request);

            if (!validaCommand.Sucesso)
            {
                return validaCommand;
            }

            return CasdastroUsuario(request);

        }
        private UsuarioResponse ValidarComando(CadastrarUsuarioCommand message)
        {
            var response = new UsuarioResponse(null, true, null);

            if (message.EhValido()) return response;

            var erros = new List<string>();
            foreach (var error in message.ValidationResult.Errors)
            {
                erros.Add(error.ErrorMessage);
            }
            response.Sucesso = false;
            response.Erros = erros;
            return response;
        }


        private UsuarioResponse CasdastroUsuario(CadastrarUsuarioCommand request)
        {
            var usuario = _usuarioRepository.CadastrarUsuario(request.ToEntity());

            if (_usuarioRepository.UnitOfWork.Commit().Result)
            {
                return new UsuarioResponse(usuario, true);
            }
            else
            {
                var erro = new List<string>();
                erro.Add("Erro ao cadastrar usuário");
                return new UsuarioResponse(null, false, erro);
            }
        }
    }
}
