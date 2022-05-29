using Cadastro_Usuarios_Domain.Entities;
using Cadastro_Usuarios_Domain.Interfaces;
using MediatR;

namespace Cadastro_Usuarios_Application.Commands.CadastrarUsuario
{
    public class DeletarUsuarioCommandHandler : IRequestHandler<DeletarUsuarioCommand, Usuario>
    {
        private readonly IMediator _mediator;
        private readonly IUsuarioRepository _usuarioRepository;

        private CancellationToken _cancellationToken;

        public DeletarUsuarioCommandHandler(IUsuarioRepository usuarioRepository,
                                              IMediator mediator)
        {
            _mediator = mediator;
            _usuarioRepository = usuarioRepository;
        }
        public async Task<Usuario> Handle(DeletarUsuarioCommand request, CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;

            if (!ValidarComando(request))
            {
                return null;
            }

            return DeletarUsuario(request);

        }
        private bool ValidarComando(DeletarUsuarioCommand message)
        {
            if (message.EhValido()) return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                _mediator.Publish(new { message = error.ErrorMessage, sucess = false, data = "" });
            }

            return false;
        }

        private Usuario DeletarUsuario(DeletarUsuarioCommand request)
        {
            var usuarioEncontrado = _usuarioRepository.BuscarUsuarioPorId(request.usuarioDTO.Id);
            if(usuarioEncontrado != null)
            {
                var usuario = _usuarioRepository.DeletarUsuario(usuarioEncontrado);

                if (_usuarioRepository.UnitOfWork.Commit().Result)
                {

                    return usuario;
                }
                else
                {

                    return null;

                }

            }
            return null;
        }
    }
}
