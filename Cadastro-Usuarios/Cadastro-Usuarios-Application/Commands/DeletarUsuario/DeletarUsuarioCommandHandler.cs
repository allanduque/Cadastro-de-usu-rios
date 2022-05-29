using Cadastro_Usuarios_Domain.Entities;
using Cadastro_Usuarios_Domain.Interfaces;
using MediatR;

namespace Cadastro_Usuarios_Application.Commands.CadastrarUsuario
{
    public class DeletarUsuarioCommandHandler : IRequestHandler<DeletarUsuarioCommand, UsuarioResponse>
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
        public async Task<UsuarioResponse> Handle(DeletarUsuarioCommand request, CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;

            var validaCommand = ValidarComando(request);

            if (!validaCommand.Sucesso)
            {
                return validaCommand;
            }

            return DeletarUsuario(request);

        }
        private UsuarioResponse ValidarComando(DeletarUsuarioCommand message)
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

        private UsuarioResponse DeletarUsuario(DeletarUsuarioCommand request)
        {
            var usuarioEncontrado = _usuarioRepository.BuscarUsuarioPorId(request.usuarioDTO.Id);
            var erro = new List<string>();
            if(usuarioEncontrado != null)
            {
                var usuario = _usuarioRepository.DeletarUsuario(usuarioEncontrado);

                if (_usuarioRepository.UnitOfWork.Commit().Result)
                {

                    return new UsuarioResponse(usuario, true);
                }
                else
                {
                    erro.Add("Erro ao deletar usuário");
                    return new UsuarioResponse(null, false, erro);
                }

            }
            erro.Add("Usuário não encontrado");
            return new UsuarioResponse(null, false, erro); 
        }
    }
}
