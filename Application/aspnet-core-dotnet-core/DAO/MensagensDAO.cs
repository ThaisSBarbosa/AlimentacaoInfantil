using System;
using System.Collections.Generic;
using System.Data;
using AlimentacaoInfantil.Enums;
using AlimentacaoInfantil.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace AlimentacaoInfantil.DAO
{
    public class MensagensDAO : IDisposable
    {
        private readonly IConfiguration _config;

        public MensagensDAO(IConfiguration configuration)
        {
            _config = configuration;
        }

        private MySqlParameter[] CriaParametros(MensagemViewModel mensagem)
        {
            MySqlParameter[] p = {
                new MySqlParameter("msg_codigo", mensagem.Codigo),
                new MySqlParameter("msg_conteudo", mensagem.Conteudo),
                new MySqlParameter("usr_codigo_remetente", mensagem.CodigoUsuarioRemetente),
                new MySqlParameter("usr_codigo_destinatario", mensagem.CodigoUsuarioDestinatario),
                new MySqlParameter("msg_status", mensagem.Status),
                new MySqlParameter("msg_data_atualizacao", mensagem.DataAtualizacao),
                new MySqlParameter("msg_respondendo_a_mensagem", mensagem.RespondendoMensagem),
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
                "msg_data_atualizacao, " +
                "msg_respondendo_a_mensagem)" +
                    "values (@msg_conteudo, " +
                    "@usr_codigo_remetente, " +
                    "@usr_codigo_destinatario, " +
                    "@msg_status, " +
                    "@msg_data_atualizacao, " +
                    "@msg_respondendo_a_mensagem)";

            HelperDAO.ExecutaSQL(sql, CriaParametros(mensagem), _config);
        }


        public void Alterar(MensagemViewModel mensagem)
        {
            string sql = "update tbMensagens set " +
                "msg_conteudo = @msg_conteudo, " +
                "usr_codigo_remetente = @usr_codigo_remetente, " +
                "usr_codigo_destinatario = @usr_codigo_destinatario, " +
                "msg_status = @msg_status, " +
                "msg_data_atualizacao = @msg_data_atualizacao, " +
                "msg_respondendo_a_mensagem = @msg_respondendo_a_mensagem " +
                "where msg_codigo  = @msg_codigo";
            HelperDAO.ExecutaSQL(sql, CriaParametros(mensagem), _config);
        }

        public void Excluir(int id)
        {
            string sql = "delete from tbMensagens where msg_codigo = " + id;
            HelperDAO.ExecutaSQL(sql, null, _config);
        }



        public MensagemViewModel Consulta(int id)
        {
            string sql = "select * from tbMensagens where msg_codigo = " + id;
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null, _config);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaModel(tabela.Rows[0]);
        }


        public List<MensagemViewModel> Lista()
        {
            string sql = "select * from tbMensagens";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null, _config);
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
            Mensagem.RespondendoMensagem = Convert.ToInt32(registro["msg_respondendo_a_mensagem"]);
            return Mensagem;
        }

        public void Dispose()
        {
            Dispose();
        }

    }
}
