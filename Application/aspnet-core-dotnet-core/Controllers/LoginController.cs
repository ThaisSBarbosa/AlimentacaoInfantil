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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(UsuarioViewModel usuario)
        {
            //UsuariosDAO usuarioDAO = new UsuariosDAO(_config);
            //int codigo = Convert.ToInt32(usuarioDAO.ConsultaCodigoPorEmailESenha(email, senha));
            //usuarioDAO.ConsultaPorCodigo(codigo);

            string sql = "select * from tbUsuarios where usr_nome = " + usuario.Nome + "and usr_senha = " + usuario.Senha;

            if (ModelState.IsValid)
            {
                var parametros = new MySqlParameter[]
                {
                new MySqlParameter("nome", usuario.Nome),
                new MySqlParameter("senha", usuario.Senha)
                };

                var tabela = HelperDAO.ExecutaProcSelect(sql, parametros, _config);

                if (tabela.Rows.Count == 0)
                {
                    ModelState.AddModelError("", "Usuário ou senha inválidos.");
                }
                else
                {
                    var codigo = tabela.Rows[0]["usr_codigo"].ToString();
                    HttpContext.Session.SetString("usr_nome", tabela.Rows[0].Field<string>("Nome"));
                    HttpContext.Session.SetString("usr_codigo", codigo);
                    return RedirectToAction("Index");
                }
            }
            return View(usuario);
        }
    }
}
