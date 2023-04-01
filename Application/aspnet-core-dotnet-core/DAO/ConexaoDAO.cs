using System;
using System.Collections.Generic;
using System.Data;
using AlimentacaoInfantil.Enums;
using AlimentacaoInfantil.Models;
using MySql.Data.MySqlClient;

namespace AlimentacaoInfantil.DAO
{
    public class ConexaoDAO : IDisposable
    {

        private MySqlParameter[] CriaParametros(ConexaoViewModel mensagem)
        {
            MySqlParameter[] p = {
                new MySqlParameter("con_codigo", mensagem.Codigo),
                new MySqlParameter("usr_codigo_1", mensagem.CodigoUsuario1),
                new MySqlParameter("usr_codigo_2", mensagem.CodigoUsuario2),
            };

            return p;
        }


        public void Inserir(ConexaoViewModel mensagem)
        {
            string sql = "insert into tbConexoes " +
                "(usr_codigo_1, " +
                "usr_codigo_2) " +
                    "values (@usr_codigo_1, " +
                    "@usr_codigo_2)";

            HelperDAO.ExecutaSQL(sql, CriaParametros(mensagem));
        }


        public void Alterar(ConexaoViewModel mensagem)
        {
            string sql = "update tbConexoes set " +
                "usr_codigo_1 = @usr_codigo_1, " +
                "usr_codigo_2 = @usr_codigo_2, " +
                "where con_codigo  = @con_codigo";
            HelperDAO.ExecutaSQL(sql, CriaParametros(mensagem));
        }

        public void Excluir(int id)
        {
            string sql = "delete from tbConexoes where con_codigo = " + id;
            HelperDAO.ExecutaSQL(sql, null);
        }



        public ConexaoViewModel ConsultaConexao(int id1, int id2)
        {
            string sql = "select * from tbConexoes where concat(usr_codigo_1 + usr_codigo_2) = " + id1 + id2 + " or " + id2 + id1;
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaModel(tabela.Rows[0]);
        }


        public List<ConexaoViewModel> Lista()
        {
            string sql = "select * from tbConexoes";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            List<ConexaoViewModel> retorno = new List<ConexaoViewModel>();

            foreach (DataRow registro in tabela.Rows)
            {
                retorno.Add(MontaModel(registro));
            }

            return retorno;
        }


        public static ConexaoViewModel MontaModel(DataRow registro)
        {
            ConexaoViewModel Mensagem = new ConexaoViewModel();
            Mensagem.Codigo = Convert.ToInt32(registro["con_codigo"]);
            Mensagem.CodigoUsuario1 = Convert.ToInt32(registro["usr_codigo_1"]);
            Mensagem.CodigoUsuario2 = Convert.ToInt32(registro["usr_codigo_2"]);
            return Mensagem;
        }

        public void Dispose()
        {
            Dispose();
        }

    }
}
