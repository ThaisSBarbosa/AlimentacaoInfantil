using System;
using System.Collections.Generic;
using System.Data;
using AlimentacaoInfantil.Enums;
using AlimentacaoInfantil.Models;
using MySql.Data.MySqlClient;

namespace AlimentacaoInfantil.DAO
{
    public class MensagensDAO : IDisposable
    {

        private MySqlParameter[] CriaParametros(MensagemViewModel mensagem)
        {
            MySqlParameter[] p = {
                new MySqlParameter("msg_codigo", mensagem.Codigo),
                new MySqlParameter("msg_conteudo", mensagem.Conteudo),
                new MySqlParameter("usr_codigo_remetente", mensagem.CodigoUsuarioRemetente),
                new MySqlParameter("usr_codigo_destinatario", mensagem.CodigoUsuarioDestinatario),
                new MySqlParameter("msg_status", mensagem.Status),
                new MySqlParameter("msg_data_atualizacao", mensagem.DataAtualizacao),
            };

            return p;
        }


        public void Inserir(MensagemViewModel mensagem)
        {
            string sql = "insert into tbMensagens " +
                "(msg_conteudo, " +
                "usr_codigo_remetente, " +
                "usr_codigo_destinatario, " +
                "msg_status, " +
                "msg_data_atualizacao) " +
                    "values (@msg_conteudo, " +
                    "@usr_codigo_remetente, " +
                    "@usr_codigo_destinatario, " +
                    "@msg_status, " +
                    "@msg_data_atualizacao)";

            HelperDAO.ExecutaSQL(sql, CriaParametros(mensagem));
        }


        public void Alterar(MensagemViewModel mensagem)
        {
            string sql = "update tbMensagens set " +
                "msg_conteudo = @msg_conteudo, " +
                "usr_codigo_remetente = @usr_codigo_remetente, " +
                "usr_codigo_destinatario = @usr_codigo_destinatario, " +
                "msg_status = @msg_status, " +
                "msg_data_atualizacao = @msg_data_atualizacao " +
                "where msg_codigo  = @msg_codigo";
            HelperDAO.ExecutaSQL(sql, CriaParametros(mensagem));
        }

        public void Excluir(int id)
        {
            string sql = "delete from tbMensagens where msg_codigo = " + id;
            HelperDAO.ExecutaSQL(sql, null);
        }



        public MensagemViewModel Consulta(int id)
        {
            string sql = "select * from tbMensagens where msg_codigo = " + id;
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaModel(tabela.Rows[0]);
        }


        public List<MensagemViewModel> Lista()
        {
            string sql = "select * from tbMensagens";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            List<MensagemViewModel> retorno = new List<MensagemViewModel>();

            foreach (DataRow registro in tabela.Rows)
            {
                retorno.Add(MontaModel(registro));
            }

            return retorno;
        }


        public static MensagemViewModel MontaModel(DataRow registro)
        {
            MensagemViewModel Mensagem = new MensagemViewModel();
            Mensagem.Codigo = Convert.ToInt32(registro["msg_codigo"]);
            Mensagem.Conteudo = registro["msg_conteudo"].ToString();
            Mensagem.CodigoUsuarioRemetente = Convert.ToInt32(registro["usr_codigo_remetente"]);
            Mensagem.CodigoUsuarioDestinatario = Convert.ToInt32(registro["usr_codigo_destinatario"]);
            Mensagem.Status = (EnumStatusMensagem)Convert.ToInt32(registro["msg_status"]);
            Mensagem.DataAtualizacao = Convert.ToDateTime(registro["msg_data_atualizacao"]);
            return Mensagem;
        }

        public void Dispose()
        {
            Dispose();
        }

    }
}
