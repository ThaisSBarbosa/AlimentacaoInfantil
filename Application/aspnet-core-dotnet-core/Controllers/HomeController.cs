using AlimentacaoInfantil.Models;
using AlimentacaoInfantil.Services;
using aspnet_core_dotnet_core.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AlimentacaoInfantil.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;

        public HomeController(IConfiguration configuration)
        {
            _config = configuration;
        }

        public IActionResult Index()
        {
            //var _userService = new UserService();
            //var user = _userService.Authenticate("luana@example.com", "1234");

            //if (user == null)
            //    return Unauthorized();

            //var token = GerarToken(user);
            //ViewBag.Token = token;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult HomeYummy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //private string GerarToken([FromBody] UserModel user)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(GetSecretKey());
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //            new Claim(ClaimTypes.Name, user.Nome)
        //        }),
        //        Expires = DateTime.Now.AddHours(5),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };

        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}

        //private string GetSecretKey()
        //{
        //    string strConn = _config.GetSection(Constantes.SECRET_KEY_LOCAL).Value;
        //    if (string.IsNullOrEmpty(strConn))
        //        strConn = Environment.GetEnvironmentVariable(Constantes.SECRET_KEY_PROD);
        //    return strConn;
        //}
    }
}
