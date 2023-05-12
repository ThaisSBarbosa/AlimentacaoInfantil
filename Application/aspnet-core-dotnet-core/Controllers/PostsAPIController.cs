using AlimentacaoInfantil.DAO;
using AlimentacaoInfantil.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlimentacaoInfantil.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsAPIController : Controller
    {
        private readonly IConfiguration _config;

        public PostsAPIController(IConfiguration configuration)
        {
            _config = configuration;
        }

        public class Post
        {
            public string conteudo { get; set; }
            public int codigo { get; set; }
            public int autor { get; set; }
            public int amei { get; set; }
            public bool anuncio { get; set; }
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
        public JsonResult FazerPost([FromBody] Post novoPost)
        {
            PostsDAO postsDAO = new PostsDAO(_config);

            PostViewModel post = new PostViewModel
            {
                Conteudo = novoPost.conteudo,
                Autor = novoPost.autor,
                Amei = novoPost.amei,
                Anuncio = novoPost.anuncio,
                Data = DateTime.Now
            };

            postsDAO.Inserir(post);
            return Json(new { retorno = "Post feito com sucesso!" });
        }

        [Authorize]
        [HttpPut("EditarPost_v1")]
        public JsonResult EditarPost([FromBody] Post novoPost)
        {
            PostsDAO postsDAO = new PostsDAO(_config);
            PostViewModel post = postsDAO.Consulta(novoPost.codigo);

            if (post == null)
                post = postsDAO.Lista().FirstOrDefault();

            if (post != null)
            {
                post.Conteudo = novoPost.conteudo;
                postsDAO.Alterar(post);
                return Json(new { retorno = "Post de código " + post.Codigo + " alterado com sucesso!" });
            }
            else
                return Json(new { retorno = "Ocorreu uma falha. Verifique se existe algum post cadastrado." });

        }

        [Authorize]
        [HttpDelete("ApagarPost_v1")]
        public JsonResult ApagarPost([FromBody] Post postParaDeletar)
        {
            PostsDAO postsDAO = new PostsDAO(_config);
            PostViewModel post = postsDAO.Consulta(postParaDeletar.codigo);

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
        public JsonResult FazerAnuncios([FromBody] Post anuncio)
        {
            PostsDAO postsDAO = new PostsDAO(_config);

            PostViewModel post = new PostViewModel
            {
                Conteudo = anuncio.conteudo,
                Autor = anuncio.autor,
                Amei = anuncio.amei,
                Anuncio = true,
                Data = DateTime.Now
            };

            postsDAO.Inserir(post);
            return Json(new { retorno = "Post e anúncio feitos com sucesso!" });
        }

        [Authorize]
        [HttpPut("EditarAnuncio_v1")]
        public JsonResult EditarAnuncio([FromBody] Post anuncio)
        {
            PostsDAO postsDAO = new PostsDAO(_config);
            PostViewModel post = postsDAO.ConsultaAnuncios(anuncio.codigo);

            if (post == null)
                post = postsDAO.ListaAnuncios().FirstOrDefault();

            if (post != null)
            {
                post.Conteudo = anuncio.conteudo;
                postsDAO.Alterar(post);
                return Json(new { retorno = "Post de código " + post.Codigo + " alterado com sucesso!" });
            }
            else
                return Json(new { retorno = "Ocorreu uma falha. Verifique se existe algum post cadastrado." });
        }

        [Authorize]
        [HttpDelete("ApagarAnuncio_v1")]
        public JsonResult ApagarAnuncio([FromBody] Post anuncio)
        {
            PostsDAO postsDAO = new PostsDAO(_config);
            PostViewModel post = postsDAO.ConsultaAnuncios(anuncio.codigo);

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
        public JsonResult EnviarAmei([FromBody] Post postAmei)
        {
            PostsDAO postsDAO = new PostsDAO(_config);
            PostViewModel post = postsDAO.Consulta(postAmei.codigo);

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
        public JsonResult RetirarAmei([FromBody] Post postAmei)
        {
            PostsDAO postsDAO = new PostsDAO(_config);
            PostViewModel post = postsDAO.Consulta(postAmei.codigo);

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