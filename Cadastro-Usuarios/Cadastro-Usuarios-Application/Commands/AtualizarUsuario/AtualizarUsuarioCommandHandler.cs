using Cadastro_Usuarios_Domain.Entities;
using Cadastro_Usuarios_Domain.Interfaces;
using MediatR;

namespace Cadastro_Usuarios_Application.Commands.CadastrarUsuario
{
    public class AtualizarUsuarioCommandHandler : IRequestHandler<AtualizarUsuarioCommand, Usuario>
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
        public async Task<Usuario> Handle(AtualizarUsuarioCommand request, CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;

            if (!ValidarComando(request))
            {
                return null;
            }

            return AtualizarUsuario(request);

        }
        private bool ValidarComando(AtualizarUsuarioCommand message)
        {
            if (message.EhValido()) return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                _mediator.Publish(new { message = error.ErrorMessage, sucess = false, data = "" });
            }

            return false;
        }

        private Usuario AtualizarUsuario(AtualizarUsuarioCommand request)
        {

                var usuario = _usuarioRepository.AtualizarUsuario(request.ToEntity());

                if (_usuarioRepository.UnitOfWork.Commit().Result)
                {

                    return usuario;
                }
                else
                {

                    return null;

                }

        }
    }
}
