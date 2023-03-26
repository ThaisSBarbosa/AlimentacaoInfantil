using System;
using System.Collections.Generic;
using System.Data;
using AlimentacaoInfantil.Models;
using MySql.Data.MySqlClient;

namespace AlimentacaoInfantil.DAO
{
    public class PostsDAO
    {

        private MySqlParameter[] CriaParametros(PostViewModel post)
        {
            MySqlParameter[] p = {
                new MySqlParameter("pst_codigo", post.Codigo),
                new MySqlParameter("pst_conteudo", post.Conteudo),
                new MySqlParameter("usr_codigo_autor", post.Autor),
                new MySqlParameter("pst_amei", post.Amei),
                new MySqlParameter("pst_anuncio", post.Anuncio),
                new MySqlParameter("pst_data", post.Data),
            };

            return p;
        }


        public void Inserir(PostViewModel post)
        {
            string sql = "insert into tbPosts " +
                "(pst_codigo, " +
                "pst_conteudo, " +
                "usr_codigo_autor, " +
                "pst_amei, " +
                "pst_anuncio, " +
                "pst_data) " +
                    "values (@pst_codigo, " +
                    "@pst_conteudo, " +
                    "@usr_codigo_autor, " +
                    "@usr_amei, " +
                    "@pst_anuncio, " +
                    "@pst_data)";

            HelperDAO.ExecutaSQL(sql, CriaParametros(post));
        }


        public void Alterar(PostViewModel post)
        {
            string sql = "update tbPosts set " +
                "pst_conteudo = @pst_conteudo, " +
                "usr_codigo_autor = @usr_codigo_autor, " +
                "pst_amei = @pst_amei, " +
                "pst_anuncio = @pst_anuncio, " +
                "pst_data = @pst_data " +
                "where pst_codigo  = @pst_codigo";
            HelperDAO.ExecutaSQL(sql, CriaParametros(post));
        }

        public void Excluir(int id)
        {
            string sql = "delete tbPosts where pst_codigo = " + id;
            HelperDAO.ExecutaSQL(sql, null);
        }



        public PostViewModel Consulta(int id)
        {
            string sql = "select * from tbPosts where pst_codigo = " + id;
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaModel(tabela.Rows[0]);
        }


        public List<PostViewModel> Lista()
        {
            string sql = "select * from tbPosts";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            List<PostViewModel> retorno = new List<PostViewModel>();

            foreach (DataRow registro in tabela.Rows)
            {
                retorno.Add(MontaModel(registro));
            }

            return retorno;
        }


        public static PostViewModel MontaModel(DataRow registro)
        {
            PostViewModel Post = new PostViewModel();
            Post.Codigo = Convert.ToInt32(registro["pst_codigo"]);
            Post.Conteudo = registro["pst_conteudo"].ToString();
            Post.Autor = Convert.ToInt32(registro["usr_codigo_autor"]);
            Post.Amei = Convert.ToInt32(registro["pst_amei"]);
            Post.Anuncio = Convert.ToBoolean(registro["pst_anuncio"]);
            Post.Data = Convert.ToDateTime(registro["pst_data"]);
            return Post;
        }



    }
}
