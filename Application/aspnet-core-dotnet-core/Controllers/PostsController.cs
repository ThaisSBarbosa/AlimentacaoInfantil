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
    }
}