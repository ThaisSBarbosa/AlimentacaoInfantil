using AlimentacaoInfantil.DAO;
using AlimentacaoInfantil.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AlimentacaoInfantil.Controllers
{
    public class PostsController : Controller
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

        [HttpGet("Posts/ExibirPosts")]
        public JsonResult ExibirPosts()
        {
            List<PostViewModel> listaPosts;
            PostsDAO postsDAO = new PostsDAO();
            listaPosts = postsDAO.Lista();

            if (listaPosts.Count == 0)
                return Json(new { retorno = "Nenhum post a ser exibido." });
            else
                return Json(listaPosts);
        }

        [HttpPost("Posts/FazerPost")]
        public JsonResult FazerPost(string conteudo, int autor, int amei, bool anuncio)
        {
            PostsDAO postsDAO = new PostsDAO();

            PostViewModel post = new PostViewModel
            {
                Conteudo = conteudo,
                Autor = autor,
                Amei = amei,
                Anuncio = anuncio,
                Data = DateTime.Now
            };

            postsDAO.Inserir(post);
            return Json(new { retorno = "Post feito com sucesso!" });
        }

        [HttpPut("Posts/EditarPost")]
        public JsonResult EditarPost(int codigo, string conteudo)
        {
            PostsDAO postsDAO = new PostsDAO();
            PostViewModel post = postsDAO.Consulta(codigo);

            if (post == null)
                post = postsDAO.Lista().FirstOrDefault();

            if (post != null)
            {
                post.Conteudo = conteudo;
                postsDAO.Alterar(post);
                return Json(new { retorno = "Post de código " + post.Codigo + " alterado com sucesso!" });
            }
            else
                return Json(new { retorno = "Ocorreu uma falha. Verifique se existe algum post cadastrado." });

        }

        [HttpDelete("Posts/ApagarPost")]
        public JsonResult ApagarPost(int codigo)
        {
            PostsDAO postsDAO = new PostsDAO();
            PostViewModel post = postsDAO.Consulta(codigo);

            if (post == null)
                post = postsDAO.Lista().FirstOrDefault();

            if (post != null)
            {
                postsDAO.Excluir(post.Codigo);
                return Json(new { retorno = "Post de código " + post.Codigo + " excluído com sucesso!" });
            }
            else
                return Json(new { retorno = "Ocorreu uma falha. Verifique se existe algum post cadastrado." });
        }

        [HttpGet("Posts/ExibirAnuncios")]
        public JsonResult ExibirAnuncios()
        {
            List<PostViewModel> listaAnuncios;
            PostsDAO postsDAO = new PostsDAO();
            listaAnuncios = postsDAO.ListaAnuncios();

            if (listaAnuncios.Count == 0)
                return Json(new { retorno = "Nenhum anúncio a ser exibido." });
            else
                return Json(listaAnuncios);
        }

        [HttpPost("Posts/FazerAnuncio")]
        public JsonResult FazerAnuncios(string conteudo, int autor, int amei)
        {
            PostsDAO postsDAO = new PostsDAO();

            PostViewModel post = new PostViewModel
            {
                Conteudo = conteudo,
                Autor = autor,
                Amei = amei,
                Anuncio = true,
                Data = DateTime.Now
            };

            postsDAO.Inserir(post);
            return Json(new { retorno = "Post e anúncio feitos com sucesso!" });
        }

        [HttpPut("Posts/EditarAnuncio")]
        public JsonResult EditarAnuncios(int codigo, bool anuncio)
        {
            PostsDAO postsDAO = new PostsDAO();
            PostViewModel post = postsDAO.ConsultaAnuncios(codigo);

            if (post == null)
                post = postsDAO.ListaAnuncios().FirstOrDefault();

            if (post != null)
            {
                post.Anuncio = anuncio;
                postsDAO.Alterar(post);
                return Json(new { retorno = "Post de código " + post.Codigo + " alterado com sucesso!" });
            }
            else
                return Json(new { retorno = "Ocorreu uma falha. Verifique se existe algum post cadastrado." });
        }

        [HttpDelete("Posts/ApagarAnuncio")]
        public JsonResult ApagarAnuncio(int codigo)
        {
            PostsDAO postsDAO = new PostsDAO();
            PostViewModel post = postsDAO.ConsultaAnuncios(codigo);

            if (post == null)
                post = postsDAO.ListaAnuncios().FirstOrDefault();

            if (post != null)
            {
                postsDAO.Excluir(post.Codigo);
                return Json(new { retorno = "Post de código " + post.Codigo + " excluído com sucesso!" });
            }
            else
                return Json(new { retorno = "Ocorreu uma falha. Verifique se existe algum post cadastrado." });
        }

        
        [HttpPost("Posts/EnviarAmei")]
        public JsonResult EnviarAmei(int codigo, int amei)
        {
            PostsDAO postsDAO = new PostsDAO();
            PostViewModel post = postsDAO.ConsultaReacao(codigo);

            if (post == null)
                post = postsDAO.ListaReacao().FirstOrDefault();

            if (post != null)
            {
                post.Amei = amei;
                postsDAO.Alterar(post);
                return Json(new { retorno = "Reação amei registrada com sucesso!" });
            }
            else
                return Json(new { retorno = "Ocorreu uma falha. Verifique se existe algum post cadastrado." });
        }

        [HttpPut("Posts/RetirarAmei")]
        public JsonResult RetirarAmei(int codigo, int amei)
        {
            PostsDAO postsDAO = new PostsDAO();
            PostViewModel post = postsDAO.ConsultaReacaoAmei(codigo);

            if (post == null)
                post = postsDAO.ListaReacaoAmei().FirstOrDefault();

            if (post != null)
            {
                post.Amei = amei;
                postsDAO.Alterar(post);
                return Json(new { retorno = "Reação amei removida com sucesso!" });
            }
            else
                return Json(new { retorno = "Ocorreu uma falha. Verifique se existe algum post cadastrado." });
        }

        [HttpPost("Posts/EnviarMensagem")]
        public JsonResult EnviarMensagem(string conteudo, int remetente, int destinatario)
        {
            MensagensDAO mensagensDAO = new MensagensDAO();

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

        [HttpPost("Posts/ResponderMensagem")]
        public JsonResult ResponderMensagem(int codigo, string conteudo, int remetente, int destinatario)
        {
            MensagensDAO mensagensDAO = new MensagensDAO();

            MensagemViewModel mensagemOriginal = mensagensDAO.Consulta(codigo);

            if (mensagemOriginal == null)
            {
                return Json(new { retorno = "Ocorreu uma falha. Verifique se existe alguma mensagem original." });

            }
            mensagemOriginal.Status = Enums.EnumStatusMensagem.RESPONDIDA;
            mensagemOriginal.DataAtualizacao = DateTime.Now;

            MensagemViewModel mensagemRespondida = new MensagemViewModel
            {
                Conteudo = conteudo,
                CodigoUsuarioRemetente = remetente,
                CodigoUsuarioDestinatario = destinatario,
                Status = Enums.EnumStatusMensagem.ENVIADA,
                DataAtualizacao = DateTime.Now,
                RespondendoMensagem = mensagemOriginal.Codigo
            };

            mensagensDAO.Alterar(mensagemOriginal);
            mensagensDAO.Inserir(mensagemRespondida);
            return Json(new { retorno = "Mensagem respondida com sucesso!" });


        }
    }
}