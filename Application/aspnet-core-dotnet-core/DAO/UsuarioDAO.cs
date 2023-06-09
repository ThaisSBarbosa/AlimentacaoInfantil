﻿using System;
using System.Collections.Generic;
using System.Data;
using AlimentacaoInfantil.Enums;
using AlimentacaoInfantil.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace AlimentacaoInfantil.DAO
{
    public class UsuarioDAO
    {
        private readonly IConfiguration _config;

        public UsuarioDAO(IConfiguration configuration)
        {
            _config = configuration;
        }

        private MySqlParameter[] CriaParametros(UsuarioViewModel usuario)
        {
            MySqlParameter[] p = {
                new MySqlParameter("usr_codigo", usuario.Codigo),
                new MySqlParameter("usr_nome", usuario.Nome),
                new MySqlParameter("usr_email", usuario.Email),
                new MySqlParameter("usr_senha", usuario.Senha),
                new MySqlParameter("usr_tipo", usuario.Tipo),
            };

            return p;
        }


        public void Inserir(UsuarioViewModel usuario)
        {
            string sql = "insert into tbUsuarios " +
                "(usr_nome, " +
                "usr_tipo, usr_email, usr_senha) " +
                    "values (@usr_nome, " +
                    "@usr_tipo, @usr_email, @usr_senha)";

            HelperDAO.ExecutaSQL(sql, CriaParametros(usuario), _config);
        }


        public void Alterar(UsuarioViewModel usuario)
        {
            string sql = "update tbUsuarios set " +
                "usr_nome = @usr_nome, " +
                "usr_tipo = @usr_tipo " +
                "usr_senha = @usr_senha " +
                "usr_email = @usr_email " +
                "where usr_codigo  = @usr_codigo";
            HelperDAO.ExecutaSQL(sql, CriaParametros(usuario), _config);
        }

        public void Excluir(int id)
        {
            string sql = "delete from tbUsuarios where usr_codigo = " + id;
            HelperDAO.ExecutaSQL(sql, null, _config);
        }



        public UsuarioViewModel Consulta(int id)
        {
            string sql = "select * from tbUsuarios where usr_codigo = " + id;
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null, _config);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaModel(tabela.Rows[0]);
        }

        public UsuarioViewModel ConsultaPorNome(string nome)
        {
            string sql = "select * from tbUsuarios where usr_nome = '" + nome + "'";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null, _config);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaModel(tabela.Rows[0]);
        }

        public UsuarioViewModel ConsultaPorCodigo(int codigo)
        {
            string sql = "select * from tbUsuarios where usr_codigo = " + codigo;
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null, _config);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaModel(tabela.Rows[0]);
        }

        public int? ConsultaCodigoPorEmailESenha(string email, string senha)
        {
            string sql = "select usr_codigo from tbUsuarios where usr_email = " + email + "and usr_senha = " + senha;
            
            var parametros = new MySqlParameter[]
            {
                new MySqlParameter("@usr_email", email),
                new MySqlParameter("@usr_senha", senha)
            };

            DataTable tabela = HelperDAO.ExecutaSelect(sql, parametros, _config);
            if (tabela.Rows.Count == 0)
                return null; 
            return Convert.ToInt32(tabela.Rows[0]["usr_codigo"]);
        }

        public UsuarioViewModel ConsultaPorNomeESenha(string nome, string senha)
        {
            string sql = "select * from tbUsuarios where usr_nome = '" + nome + "' and usr_senha = '" + senha + "'";

            var parametros = new MySqlParameter[]
            {
                new MySqlParameter("@usr_email", nome),
                new MySqlParameter("@usr_senha", senha)
            };

            DataTable tabela = HelperDAO.ExecutaSelect(sql, parametros, _config);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaModel(tabela.Rows[0]);
        }

        public List<UsuarioViewModel> Lista()
        {
            string sql = "select * from tbUsuarios";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null, _config);
            List<UsuarioViewModel> retorno = new List<UsuarioViewModel>();

            foreach (DataRow registro in tabela.Rows)
            {
                retorno.Add(MontaModel(registro));
            }

            return retorno;
        }


        public static UsuarioViewModel MontaModel(DataRow registro)
        {
            UsuarioViewModel Usuario = new UsuarioViewModel();
            Usuario.Codigo = Convert.ToInt32(registro["usr_codigo"]);
            Usuario.Nome = registro["usr_nome"].ToString();
            Usuario.Email = registro["usr_email"].ToString();
            Usuario.Senha = registro["usr_senha"].ToString();
            Usuario.Tipo = (EnumTipoUsuario)Convert.ToInt32(registro["usr_tipo"]);
            return Usuario;
        }
    }
}
