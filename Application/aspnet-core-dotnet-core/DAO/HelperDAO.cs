using System.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace AlimentacaoInfantil.DAO
{
    public static class HelperDAO
    {
        public static void ExecutaSQL(string sql, MySqlParameter[] parametros, IConfiguration config)
        {
            using (MySqlConnection conexao = ConexaoBD.GetConexao(config))
            {
                using (MySqlCommand comando = new MySqlCommand(sql, conexao))
                {
                    if (parametros != null)
                        comando.Parameters.AddRange(parametros);
                    comando.ExecuteNonQuery();
                }
                conexao.Close();
            }
        }


        /// <summary>
        /// Executa uma instrução Select
        /// </summary>
        /// <param name="sql">instrução SQL</param>
        /// <returns>DataTable com os dados da instrução SQL</returns>
        public static DataTable ExecutaSelect(string sql, MySqlParameter[] parametros, IConfiguration config)
        {
            using (MySqlConnection conexao = ConexaoBD.GetConexao(config))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conexao))
                {
                    if (parametros != null)
                        adapter.SelectCommand.Parameters.AddRange(parametros);
                    DataTable tabelaTemp = new DataTable();
                    adapter.Fill(tabelaTemp);
                    conexao.Close();
                    return tabelaTemp;
                }
            }
        }
    }
}
