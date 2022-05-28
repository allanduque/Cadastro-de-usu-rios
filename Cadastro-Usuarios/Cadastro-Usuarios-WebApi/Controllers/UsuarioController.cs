using Cadastro_Usuarios_Domain.Bases;
using Cadastro_Usuarios_Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cadastro_Usuarios_WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : Cadastro_Usuarios_Domain.Bases.ControllerBase
    {
        private readonly IMediator _mediatorHandler;
        public UsuarioController(IMediator mediatorHandler, IHttpContextAccessor httpContextAccessor) : base(mediatorHandler, httpContextAccessor)
        {
            _mediatorHandler = mediatorHandler;
        }
        [HttpGet("BuscarTodosUsuarios")]
        public async Task<IActionResult> BuscarTodosUsuarios()
        {
            return Response();          
        }

        [HttpPost("CadastrarUsuario")]
        public async Task<IActionResult> CadastrarUsuario()
        {
            return Response();
        }

        [HttpDelete("DeletarUsuario")]
        public async Task<IActionResult> DeletarUsuario()
        {
            return Response();
        }
    }
}
