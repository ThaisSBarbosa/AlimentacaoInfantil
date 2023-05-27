using AlimentacaoInfantil.DAO;
using AlimentacaoInfantil.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AlimentacaoInfantil.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioAPIController : Controller
    {
        private readonly IConfiguration _config;

        public UsuarioAPIController(IConfiguration configuration)
        {
            _config = configuration;
        }

        [HttpPost("AutenticarUsuario_v1")]
        public JsonResult AutenticarUsuario([FromBody] UserModel user)
        {
            var usuarioViewModel = new UsuarioViewModel 
            { 
                Nome = user.Nome,
                Senha = user.Senha
            };

            var tokenEId = (new AutenticacaoController(_config)).Authenticate(usuarioViewModel);
            return Json(tokenEId);
        }
    }
}