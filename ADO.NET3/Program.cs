using System.Data;
using System.Data.SqlClient;

namespace ADO.NET3
{
    class Program
    {
        static void ShowDataTable(DataTable table)
        {
            Console.WriteLine($"Tên bảng: {table.TableName}");
            Console.Write($"{"index",15}");
            foreach (DataColumn column in table.Columns)
            {
                Console.Write($"{column.ColumnName,15}");

            }
            Console.WriteLine();
            int numberColumn = table.Columns.Count;
            int index = 0;
            foreach (DataRow row in table.Rows)
            {
                Console.Write($"{index,15}");
                for(int i = 0; i < numberColumn; i++)
                {
                    Console.Write($"{row[i],15}");
                }
                index++;
                //Console.Write($"{row[0],20}");
                //Console.Write($"{row["Name"],20}");
                //Console.Write($"{row[2],20}");
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var sqlStringBuiler = new SqlConnectionStringBuilder();
            sqlStringBuiler["Server"] = "localhost,1433";
            sqlStringBuiler["Database"] = "xtlab";
            sqlStringBuiler["UID"] = "sa";
            sqlStringBuiler["PWD"] = "Password123";
            var sqlStringConnection = sqlStringBuiler.ToString();
            using var connection = new SqlConnection(sqlStringConnection);
            connection.Open();

            //var dataset = new DataSet();
            ////dataset.Tables
            //var table = new DataTable("MyTable");
            //dataset.Tables.Add(table);

            //table.Columns.Add("STT");
            //table.Columns.Add("Name");
            //table.Columns.Add("Age");

            //table.Rows.Add(1,"Nguyen Van A",19);
            //table.Rows.Add(2, "Nguyen Van B",20);
            //table.Rows.Add(3, "Nguyen Van C",21);
            //ShowDataTable(table);
            var adapter = new SqlDataAdapter();
            adapter.TableMappings.Add("Table", "Shippers");
            // Select Command
            adapter.SelectCommand = new SqlCommand("select ShipperID,Hoten from Shippers ", connection);
            //Insert Command
            adapter.InsertCommand = new SqlCommand("insert into Shippers(Hoten) values(@Hoten) ", connection);
            adapter.InsertCommand.Parameters.Add("@Hoten", SqlDbType.NVarChar, 255, "Hoten");
            //Delete Command
            adapter.DeleteCommand = new SqlCommand("delete from Shippers where ShipperID = @ShipperID  ",connection);
            var pr1 = adapter.DeleteCommand.Parameters.Add(new SqlParameter("@ShipperID", SqlDbType.Int));
            pr1.SourceColumn = "ShipperID";
            pr1.SourceVersion = DataRowVersion.Original;
            //Update Command
            adapter.UpdateCommand = new SqlCommand("update Shippers set Hoten=@Hoten where ShipperID=@ShipperID", connection);
            var pr2 = adapter.UpdateCommand.Parameters.Add(new SqlParameter("@ShipperID", SqlDbType.Int));
            pr2.SourceColumn = "ShipperID";
            pr2.SourceVersion = DataRowVersion.Original;
            adapter.UpdateCommand.Parameters.Add("@Hoten", SqlDbType.NVarChar, 255, "Hoten");


            var dataset = new DataSet();
            adapter.Fill(dataset);
            DataTable table= dataset.Tables["Shippers"];
            //them row moi
            //var row = table.Rows.Add();
            //row["Hoten"] = "Nguyen Van O";
            //ShowDataTable(table);

            //delete row
            //var row = table.Rows[3];
            //row.Delete();

            //update
            var r = table.Rows[2];
            r["Hoten"] = "Boop";


            //cap nhat lai db
            adapter.Update(dataset);
            

            ShowDataTable(table);

            connection.Close();
        }
    }


}

