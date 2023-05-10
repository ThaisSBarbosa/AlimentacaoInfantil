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
    public class PostsController : Controller
    {
        private readonly IConfiguration _config;

        public PostsController(IConfiguration configuration)
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
        [HttpGet("ExibirPosts_v1")]
        public JsonResult ExibirPosts()
        {
            List<PostViewModel> listaPosts;
            PostsDAO postsDAO = new PostsDAO(_config);
            listaPosts = postsDAO.Lista();

            if (listaPosts.Count == 0)
                return Json(new { retorno = "Nenhum post a ser exibido." });
            else
                return Json(listaPosts);
        }

        [Authorize]
        [HttpPost("FazerPost_v1")]
        public JsonResult FazerPost(string conteudo, int autor, int amei, bool anuncio)
        {
            PostsDAO postsDAO = new PostsDAO(_config);

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

        [Authorize]
        [HttpPut("EditarPost_v1")]
        public JsonResult EditarPost(int codigo, string conteudo)
        {
            PostsDAO postsDAO = new PostsDAO(_config);
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

        [Authorize]
        [HttpDelete("ApagarPost_v1")]
        public JsonResult ApagarPost(int codigo)
        {
            PostsDAO postsDAO = new PostsDAO(_config);
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

        [Authorize]
        [HttpGet("ExibirAnuncios_v1")]
        public JsonResult ExibirAnuncios()
        {
            List<PostViewModel> listaAnuncios;
            PostsDAO postsDAO = new PostsDAO(_config);
            listaAnuncios = postsDAO.ListaAnuncios();

            if (listaAnuncios.Count == 0)
                return Json(new { retorno = "Nenhum anúncio a ser exibido." });
            else
                return Json(listaAnuncios);
        }

        [Authorize]
        [HttpPost("FazerAnuncio_v1")]
        public JsonResult FazerAnuncios(string conteudo, int autor, int amei)
        {
            PostsDAO postsDAO = new PostsDAO(_config);

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

        [Authorize]
        [HttpPut("EditarAnuncio_v1")]
        public JsonResult EditarAnuncio(int codigo, string conteudo)
        {
            PostsDAO postsDAO = new PostsDAO(_config);
            PostViewModel post = postsDAO.ConsultaAnuncios(codigo);

            if (post == null)
                post = postsDAO.ListaAnuncios().FirstOrDefault();

            if (post != null)
            {
                post.Conteudo = conteudo;
                postsDAO.Alterar(post);
                return Json(new { retorno = "Post de código " + post.Codigo + " alterado com sucesso!" });
            }
            else
                return Json(new { retorno = "Ocorreu uma falha. Verifique se existe algum post cadastrado." });
        }

        [Authorize]
        [HttpDelete("ApagarAnuncio_v1")]
        public JsonResult ApagarAnuncio(int codigo)
        {
            PostsDAO postsDAO = new PostsDAO(_config);
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

        [Authorize]
        [HttpPut("EnviarAmei_v1")]
        public JsonResult EnviarAmei(int codigo)
        {
            PostsDAO postsDAO = new PostsDAO(_config);
            PostViewModel post = postsDAO.Consulta(codigo);

            if (post == null)
                post = postsDAO.Lista().FirstOrDefault();


            if (post != null)
            {
                post.Amei++;
                postsDAO.Alterar(post);
                return Json(new { retorno = "Reação amei registrada com sucesso!" });
            }
            else
                return Json(new { retorno = "Ocorreu uma falha. Verifique se existe algum post cadastrado." });
        }

        [Authorize]
        [HttpPut("RetirarAmei_v1")]
        public JsonResult RetirarAmei(int codigo)
        {
            PostsDAO postsDAO = new PostsDAO(_config);
            PostViewModel post = postsDAO.Consulta(codigo);

            if (post == null)
                post = postsDAO.Lista().FirstOrDefault();

            if (post != null)
            {
                post.Amei--;
                postsDAO.Alterar(post);
                return Json(new { retorno = "Reação amei removida com sucesso!" });
            }
            else
                return Json(new { retorno = "Ocorreu uma falha. Verifique se existe algum post cadastrado." });
        }
    }
}