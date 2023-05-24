using AlimentacaoInfantil.DAO;
using AlimentacaoInfantil.Enums;
using AlimentacaoInfantil.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace AlimentacaoInfantil.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConexoesAPIController : Controller
    {
        private readonly IConfiguration _config;

        public ConexoesAPIController(IConfiguration configuration)
        {
            _config = configuration;
        }

        public class Conexao
        {
            public int codigo { get; set; }
            public int codigoUsuario1 { get; set; }
            public int codigoUsuario2 { get; set; }
        }

        [Authorize]
        [HttpPost("ConectarSeAUmPai_v1")]
        public JsonResult ConectarSeAUmPai([FromBody] Conexao con)
        {
            ConexaoDAO conexaoDAO = new ConexaoDAO(_config);
            UsuarioDAO usuarioDAO = new UsuarioDAO(_config);

            // crio dois usuarios para não ficar tentando conectar dois usuarios ja conectados 
            UsuarioViewModel novoUsuario1 = new UsuarioViewModel
            {
                Nome = "UsuarioTeste1" + DateTime.Now.ToShortTimeString().Replace(":", string.Empty),
                Tipo = EnumTipoUsuario.PAIS
            };
            usuarioDAO.Inserir(novoUsuario1);
            UsuarioViewModel novoUsuario1ComCodigo = usuarioDAO.ConsultaPorNome(novoUsuario1.Nome);

            UsuarioViewModel novoUsuario2 = new UsuarioViewModel
            {
                Nome = "UsuarioTeste2" + DateTime.Now.ToShortTimeString().Replace(":", string.Empty),
                Tipo = EnumTipoUsuario.PAIS
            };
            usuarioDAO.Inserir(novoUsuario2);
            UsuarioViewModel novoUsuario2ComCodigo = usuarioDAO.ConsultaPorNome(novoUsuario2.Nome);

            ConexaoViewModel conexao = new ConexaoViewModel
            {
                CodigoUsuario1 = novoUsuario1ComCodigo.Codigo,
                CodigoUsuario2 = novoUsuario2ComCodigo.Codigo
            };

            conexaoDAO.Inserir(conexao);
            return Json(new { retorno = "Conexão entre " + novoUsuario1.Nome + " e " + novoUsuario2.Nome + " feita com sucesso!" });
        }
    }
}