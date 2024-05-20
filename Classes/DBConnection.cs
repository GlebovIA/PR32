using System.Data;
using System.Data.SqlClient;

namespace PR32.Classes
{
    public class DBConnection
    {
        public static DataTable Connection(string query)
        {
            DataTable dataTable = new DataTable("Datatable");
            SqlConnection sqlConnection = new SqlConnection(@"server=HOME-PC\MYSERVER;Trusted_Connection=No;DataBase=VinylRecords;Integrated Security=True;");
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = query;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
    }
}
