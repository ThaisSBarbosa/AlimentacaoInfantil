using AlimentacaoInfantil.Models;
using AlimentacaoInfantil.Services;
using aspnet_core_dotnet_core.Utils;
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

        private string GerarToken(UsuarioViewModel user)
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

        public string Authenticate(UsuarioViewModel usuarioViewModel)
        {
            var _userService = new UserService(_config);
            var user = _userService.Authenticate(usuarioViewModel.Nome, usuarioViewModel.Senha);

            if (user == null)
                return string.Empty;

            var token = GerarToken(user);
            return token;
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
