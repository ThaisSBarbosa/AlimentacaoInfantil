using AlimentacaoInfantil.DAO;
using AlimentacaoInfantil.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AlimentacaoInfantil.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MensagensController : Controller
    {
        private readonly IConfiguration _config;

        public MensagensController(IConfiguration configuration)
        {
            _config = configuration;
        }

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

        [Authorize]
        [HttpPost("EnviarMensagem_v1")]
        public JsonResult EnviarMensagem(string conteudo, int remetente, int destinatario)
        {
            MensagensDAO mensagensDAO = new MensagensDAO(_config);

            MensagemViewModel mensagem = new MensagemViewModel
            {
                Conteudo = conteudo,
                CodigoUsuarioRemetente = remetente,
                CodigoUsuarioDestinatario = destinatario,
                Status = Enums.EnumStatusMensagem.ENVIADA,
                DataAtualizacao = DateTime.Now,
            };

            mensagensDAO.Inserir(mensagem);
            return Json(new { retorno = "Mensagem enviada com sucesso!" });
        }

        [Authorize]
        [HttpPost("ResponderMensagem_v1")]
        public JsonResult ResponderMensagem(int codigo, string conteudo, int remetente, int destinatario)
        {
            MensagensDAO mensagensDAO = new MensagensDAO(_config);

            MensagemViewModel mensagemOriginal = mensagensDAO.Consulta(codigo);
            if (mensagemOriginal == null)
            {
                return Json(new { retorno = "Ocorreu uma falha. Verifique se existe alguma mensagem para ser respondida." });
            }
            else
            {
                mensagemOriginal.Status = Enums.EnumStatusMensagem.RESPONDIDA;
                mensagemOriginal.DataAtualizacao = DateTime.Now;

                MensagemViewModel mensagemResposta = new MensagemViewModel
                {
                    Conteudo = conteudo,
                    CodigoUsuarioRemetente = remetente,
                    CodigoUsuarioDestinatario = destinatario,
                    Status = Enums.EnumStatusMensagem.ENVIADA,
                    DataAtualizacao = DateTime.Now,
                    RespondendoMensagem = mensagemOriginal.Codigo
                };

                mensagensDAO.Alterar(mensagemOriginal);
                mensagensDAO.Inserir(mensagemResposta);
                return Json(new { retorno = "Mensagem respondida com sucesso!" });
            }
        }
    }
}