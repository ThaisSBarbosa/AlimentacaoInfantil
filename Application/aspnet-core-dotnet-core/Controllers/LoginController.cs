using AlimentacaoInfantil.DAO;
using AlimentacaoInfantil.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace aspnet_core_dotnet_core.Controllers
{
    public class LoginController : Controller
    {

        private readonly IConfiguration _config;

        public LoginController(IConfiguration configuration)
        {
            _config = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult Login(UsuarioViewModel usuario)
        //{
            //UsuarioDAO usuarioDAO = new UsuarioDAO(_config);
            //var usuarioEncontrado = usuarioDAO.ConsultaPorNomeESenha(usuario.Nome, usuario.Senha);

            //return View(usuarioEncontrado);
        //}
    }
}
