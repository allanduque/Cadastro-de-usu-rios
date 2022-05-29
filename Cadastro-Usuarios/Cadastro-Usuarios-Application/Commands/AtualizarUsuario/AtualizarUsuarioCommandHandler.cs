using Cadastro_Usuarios_Domain.Entities;
using Cadastro_Usuarios_Domain.Interfaces;
using MediatR;

namespace Cadastro_Usuarios_Application.Commands.CadastrarUsuario
{
    public class AtualizarUsuarioCommandHandler : IRequestHandler<AtualizarUsuarioCommand, UsuarioResponse>
    {
        private readonly IMediator _mediator;
        private readonly IUsuarioRepository _usuarioRepository;

        private CancellationToken _cancellationToken;

        public AtualizarUsuarioCommandHandler(IUsuarioRepository usuarioRepository,
                                              IMediator mediator)
        {
            _mediator = mediator;
            _usuarioRepository = usuarioRepository;
        }
        public async Task<UsuarioResponse> Handle(AtualizarUsuarioCommand request, CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;

            var validaCommand = ValidarComando(request);

            if (!validaCommand.Sucesso)
            {
                return validaCommand;
            }

            return AtualizarUsuario(request);

        }
        private UsuarioResponse ValidarComando(AtualizarUsuarioCommand message)
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

        private UsuarioResponse AtualizarUsuario(AtualizarUsuarioCommand request)
        {

                var usuario = _usuarioRepository.AtualizarUsuario(request.ToEntity());

                if (_usuarioRepository.UnitOfWork.Commit().Result)
                {

                    return new UsuarioResponse(usuario, true);

                }
                else
                {

                    var erro = new List<string>();
                    erro.Add("Erro ao atualizar usuário");
                    return new UsuarioResponse(null, false, erro);

                }

        }
    }
}
