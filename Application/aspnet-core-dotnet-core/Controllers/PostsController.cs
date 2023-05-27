using AlimentacaoInfantil.DAO;
using AlimentacaoInfantil.Enums;
using AlimentacaoInfantil.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace AlimentacaoInfantil.Controllers
{
    public class PostsController : Controller
    {
        private readonly IConfiguration _config;

        public PostsController(IConfiguration configuration)
        {
            _config = configuration;
        }

        public IActionResult Index()
        {
            JObject json = JObject.Parse(JsonConvert.SerializeObject((new PostsAPIController(_config)).ExibirPosts()));
            var lista = json["Value"].ToObject<List<PostViewModel>>();

            foreach(var post in lista)
            {
                UsuarioDAO UsuarioDAO = new UsuarioDAO(_config);
                post.UsuarioAutor = UsuarioDAO.ConsultaPorCodigo(post.Autor);
                post.TipoUsuario = post.UsuarioAutor.Tipo.GetDescription();
            }

            return View(lista.OrderByDescending(p => p.Data).ToList());
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