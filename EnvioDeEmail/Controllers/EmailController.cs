using FluentEmail.Core;
using Microsoft.AspNetCore.Mvc;

namespace EnvioDeEmail.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IFluentEmail _fluentEmail;

        public EmailController(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
        }

        [HttpPost]
        public async Task<ActionResult> EnviarEmail([FromBody] string destinatario)
        {

            var enviar = await _fluentEmail.To(destinatario.Trim().ToLower())
                               .Body("The body")
                               .SendAsync();

            if (!enviar.Successful)
            {
                return BadRequest($"Ocorreu um problema ao enviar e-mail para : {destinatario}");
            }

            return Ok("E-mail enviado com sucesso");
        }
    }
}
