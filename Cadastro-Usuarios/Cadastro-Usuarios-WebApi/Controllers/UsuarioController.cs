using Cadastro_Usuarios_Application.Commands.CadastrarUsuario;
using Cadastro_Usuarios_Domain.Bases;
using Cadastro_Usuarios_Domain.DTOs;
using Cadastro_Usuarios_Domain.Entities;
using Cadastro_Usuarios_Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cadastro_Usuarios_WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : Cadastro_Usuarios_Domain.Bases.ControllerBase
    {
        private readonly IMediator _mediatorHandler;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IMediator mediatorHandler, IHttpContextAccessor httpContextAccessor, IUsuarioRepository usuarioRepository) : base(mediatorHandler, httpContextAccessor)
        {
            _mediatorHandler = mediatorHandler;
            _usuarioRepository = usuarioRepository;
        }
        [HttpGet("BuscarTodosUsuarios")]
        public IActionResult BuscarTodosUsuarios()
        {
            return Ok(_usuarioRepository.BuscarTodosUsuarios());          
        }

        [HttpPost("CadastrarUsuario")]
        public async Task<IActionResult> CadastrarUsuario([FromBody] UsuarioDTO usuario)
        {
            var command = new CadastrarUsuarioCommand(usuario);
            
            var result = await _mediatorHandler.Send(command);

            return (result.Sucesso) ? Ok(result.Usuario) : BadRequest(result.Erros); ;

        }
        [HttpDelete("DeletarUsuarioPorId")]
        public async Task<IActionResult> DeletarUsuarioPorId(int Id)
        {
            var command = new DeletarUsuarioCommand(Id);

            var result = await _mediatorHandler.Send(command);

            return (result.Sucesso) ? Ok(result.Usuario) : BadRequest(result.Erros); ;
        }

        [HttpDelete("DeletarUsuario")]
        public async Task<IActionResult> DeletarUsuario([FromBody] UsuarioDTO usuario)
        {
            var command = new DeletarUsuarioCommand(usuario);

            var result = await _mediatorHandler.Send(command);

            return (result.Sucesso) ? Ok(result.Usuario) : BadRequest(result.Erros); ;
        }

        [HttpPut("AtualizarUsuario")]
        public async Task<IActionResult> AtualizarUsuario([FromBody] UsuarioDTO usuario)
        {
            var command = new AtualizarUsuarioCommand(usuario);

            var result = await _mediatorHandler.Send(command);

            return (result.Sucesso) ? Ok(result.Usuario) : BadRequest(result.Erros); ;
        }
    }
}
