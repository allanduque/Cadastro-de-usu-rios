using Cadastro_Usuarios_Domain.Entities;
using Cadastro_Usuarios_Domain.Interfaces;
using MediatR;

namespace Cadastro_Usuarios_Application.Commands.CadastrarUsuario
{
    public class CadastrarUsuarioCommandHandler : IRequestHandler<CadastrarUsuarioCommand, Usuario>
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
        public async Task<Usuario> Handle(CadastrarUsuarioCommand request, CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;

            if (!ValidarComando(request))
            {
                return null;
            }

            return CasdastroUsuario(request);

        }
        private bool ValidarComando(CadastrarUsuarioCommand message)
        {
            if (message.EhValido()) return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                _mediator.Publish(new { message = error.ErrorMessage, sucess = false, data = "" });
            }

            return false;
        }

        private Usuario CasdastroUsuario(CadastrarUsuarioCommand request)
        {
            var usuario = _usuarioRepository.CadastrarUsuario(request.ToEntity());

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
