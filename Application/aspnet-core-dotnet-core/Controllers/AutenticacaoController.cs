using AlimentacaoInfantil.Models;
using AlimentacaoInfantil.Services;
using aspnet_core_dotnet_core.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AlimentacaoInfantil.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AutenticacaoController(IConfiguration configuration)
        {
            _config = configuration;
        }

        private string GerarToken([FromBody] UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(GetSecretKey());
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Nome)
                }),
                Expires = DateTime.Now.AddHours(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [AllowAnonymous]
        [HttpPost("AutenticarUsuario_v1")]
        public IActionResult Authenticate([FromBody] UserModel userCredentials)
        {
            var _userService = new UserService();
            var user = _userService.Authenticate(userCredentials.Email, userCredentials.Senha);

            if (user == null)
                return Unauthorized();

            var token = GerarToken(user);
            return Ok(new { Token = token });
        }

        private string GetSecretKey()
        {
            string strConn = _config.GetSection(Constantes.SECRET_KEY_LOCAL).Value;
            if (string.IsNullOrEmpty(strConn))
                strConn = Environment.GetEnvironmentVariable(Constantes.SECRET_KEY_PROD);
            return strConn;
        }
    }
}
