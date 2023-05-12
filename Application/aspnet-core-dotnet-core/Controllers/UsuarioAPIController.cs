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
    }
}