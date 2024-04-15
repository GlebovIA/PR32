using System.Data;
using System.Data.SqlClient;

namespace PR32.Classes
{
    public class DBConnection
    {
        public static DataTable Connection(string query)
        {
            DataTable dataTable = new DataTable("Datatable");
            SqlConnection sqlConnection = new SqlConnection("server=127.0.0.1;port=3306;uid=root;pwd=;DataBase=;Trusted_Connection=No");
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = query;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
    }
}
