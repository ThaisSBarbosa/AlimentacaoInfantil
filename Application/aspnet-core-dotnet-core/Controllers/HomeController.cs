using AlimentacaoInfantil.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using static Org.BouncyCastle.Math.EC.ECCurve;

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
            ViewBag.StrCon = _config.GetSection("ConnectionString").Value;
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
    }
}