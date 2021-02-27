using Microsoft.AspNetCore.Mvc;

namespace WebApiCore.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        protected ActionResult CustomResponse(object result = null)
        {
            if (ValidOperation())
                return Ok(new
                {
                    success = true,
                    data = result
                });

            return BadRequest(new
            {
                success = false,
                erros = ObterErros()
            });
        }

        public bool ValidOperation()
        {
            // Validações
            return true;
        }

        protected string ObterErros()
        {
            return "";
        }
    }
}