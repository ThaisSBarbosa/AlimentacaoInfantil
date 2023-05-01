using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

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
            string strConn = config.GetSection("ConnectionString").Value;
            MySqlConnection conexao = new MySqlConnection(strConn);
            conexao.Open();
            return conexao;
        }
    }
}
