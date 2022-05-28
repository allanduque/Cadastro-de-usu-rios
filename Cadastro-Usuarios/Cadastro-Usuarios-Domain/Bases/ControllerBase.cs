using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cadastro_Usuarios_Domain.Bases
{
    [Route("api/[controller]")]
    public abstract class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        private readonly IMediator _mediatorHandler;

        protected Guid ClienteId;

        protected ControllerBase(IMediator mediatorHandler,
                                 IHttpContextAccessor httpContextAccessor)
        {
            _mediatorHandler = mediatorHandler;
        }



        protected new IActionResult Response(object result = null, bool sucess = true, string message = "")
        {
            if (sucess)
            {
                return Ok(new
                {
                    success = true,
                    data = result,
                    message = message
                });
            }          

            return BadRequest(new
            {
                success = false,
                errors = message
            });
        }
    }
}
