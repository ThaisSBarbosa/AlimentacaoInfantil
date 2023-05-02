using aspnet_core_dotnet_core.Utils;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;

namespace AlimentacaoInfantil.DAO
{
    public static class ConexaoBD
    {
        /// <summary>
        /// Método estático que retorna um conexao aberta com o BD.
        /// </summary>
        /// <returns>Conexão aberta</returns>
        public static MySqlConnection GetConexao(IConfiguration config)
        {
            MySqlConnection conexao = new MySqlConnection(GetConnectionString(config));
            conexao.Open();
            return conexao;
        }

        private static string GetConnectionString(IConfiguration config)
        {
            string strConn = config.GetSection(Constantes.STR_CONN_LOCAL).Value;
            if (string.IsNullOrEmpty(strConn))
                strConn = Environment.GetEnvironmentVariable(Constantes.STR_CONN_AZURE);
            return strConn;
        }
    }
}
