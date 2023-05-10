using AlimentacaoInfantil.Models;
using AlimentacaoInfantil.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AlimentacaoInfantil.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [Authorize]
        [HttpGet("exemplo")]
        public IActionResult Exemplo()
        {
            return Ok("Exemplo de rota protegida com JWT.");
        }

        [HttpPost("token")]
        private string Token([FromBody] UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("202305091337_secret_key_projeto_ec8_202305091337");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Nome)
                }),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [AllowAnonymous]
        [HttpPost("autenticar")]
        public IActionResult Authenticate([FromBody] UserModel credentials)
        {
            var _userService = new UserService();
            var user = _userService.Authenticate("teste@example.com", "1234");

            if (user == null)
                return Unauthorized();

            var token = Token(user);
            return Ok(new { Token = token });
        }
    }
}
