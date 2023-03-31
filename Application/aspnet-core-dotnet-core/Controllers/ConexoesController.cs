﻿using AlimentacaoInfantil.DAO;
using AlimentacaoInfantil.Enums;
using AlimentacaoInfantil.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AlimentacaoInfantil.Controllers
{
    public class ConexoesController : Controller
    {
        public IActionResult Index()
        {
            return View();
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

        [HttpPost("Conexoes/ConectarSeAUmPai")]
        public JsonResult ConectarSeAUmPai(int usuario1, int usuario2)
        {
            ConexaoDAO conexaoDAO = new ConexaoDAO();
            UsuariosDAO usuarioDAO = new UsuariosDAO();

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