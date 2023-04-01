using MySql.Data.MySqlClient;

namespace AlimentacaoInfantil.DAO
{
    public static class ConexaoBD
    {
        /// <summary>
        /// Método Estático que retorna um conexao aberta com o BD
        /// </summary>
        /// <returns>Conexão aberta</returns>
        public static MySqlConnection GetConexao()
        {
            //string strCon = "Data Source=fesa-alimentacaoinfantil.mysql.database.azure.com\\fesa; Database=alimentacaoinfantil; integrated security=true";
            string strCon = "server=fesa-alimentacaoinfantil.mysql.database.azure.com;uid=fesa;pwd=Grupoprojeto@ec8;database=alimentacaoinfantil";
            MySqlConnection conexao = new MySqlConnection(strCon);
            conexao.Open();
            return conexao;
        }
    }
}
