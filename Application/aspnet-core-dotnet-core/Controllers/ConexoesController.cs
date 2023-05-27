using AlimentacaoInfantil.DAO;
using AlimentacaoInfantil.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using AlimentacaoInfantil.Enums;

namespace AlimentacaoInfantil.Controllers
{
    public class ConexoesController : Controller
    {
        private readonly IConfiguration _config;

        public ConexoesController(IConfiguration configuration)
        {
            _config = configuration;
        }

        public IActionResult Index()
        {
            UsuarioDAO usuarioDAO = new UsuarioDAO(_config);
            var usuarios = usuarioDAO.Lista();

            foreach (var usuario in usuarios)
                usuario.DescricaoTipo = usuario.Tipo.GetDescription();

            return View(usuarios);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}