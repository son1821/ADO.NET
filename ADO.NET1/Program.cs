using System.Data.Common;
using System.Data.SqlClient;

namespace ADO.NET1
{
    class Program
    {
        static void Main(string[] args)
        {
            var sqlStringBuiler = new SqlConnectionStringBuilder();
            sqlStringBuiler["Server"] = "localhost,1433";
            sqlStringBuiler["Database"] = "xtlab";
            sqlStringBuiler["UID"] = "sa";
            sqlStringBuiler["PWD"] = "Password123";
            var sqlStringConnection = sqlStringBuiler.ToString();
            Console.WriteLine(sqlStringConnection);

            //string sqlStringConnection = "Data Source=localhost,1433;Initial Catalog =xtlab;User ID=sa;Password=Password123";
            using var connection = new SqlConnection(sqlStringConnection);
            Console.WriteLine(connection.State);
            connection.Open();
            Console.WriteLine(connection.State);
            //truy van..
            using DbCommand command = new SqlCommand();
            command.Connection =   connection;
            command.CommandText = "SELECT TOP (10) * FROM Sanpham";
            var datareader = command.ExecuteReader();
            while (datareader.Read())
            {
                Console.WriteLine($"{datareader["TenSanpham"], 10}, Gia {datareader["Gia"], 8}");
            }
            connection.Close();
        }
    }
}
