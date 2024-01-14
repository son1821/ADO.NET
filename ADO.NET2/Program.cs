using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text.Unicode;

namespace ADO.NET2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var sqlStringBuiler = new SqlConnectionStringBuilder();
            sqlStringBuiler["Server"] = "localhost,1433";
            sqlStringBuiler["Database"] = "xtlab";
            sqlStringBuiler["UID"] = "sa";
            sqlStringBuiler["PWD"] = "Password123";
            var sqlStringConnection = sqlStringBuiler.ToString();
          

            //string sqlStringConnection = "Data Source=localhost,1433;Initial Catalog =xtlab;User ID=sa;Password=Password123";
            using var connection = new SqlConnection(sqlStringConnection);
           
            connection.Open();
           using var command = new SqlCommand();
            command.Connection = connection;
            //command.CommandText = "SELECT KhachhangID,HoTen,Diachi FROM Khachhang WHERE KhachhangID >@KhachhangID";
            //var khachhangid = new SqlParameter("@KhachhangID",5);
            //command.Parameters.Add(khachhangid);
            //var khachahngid = command.Parameters.AddWithValue("@KhachhangID", 5);
            //khachahngid.Value = 10;
            //command.EndExecuteReader; - Dùng khi kết quả trả về có nhiều dòng

            //using var sqlreader =  command.ExecuteReader();
            // var datatable = new DataTable();
            // datatable.Load(sqlreader);

            //if (sqlreader.HasRows)
            //{
            //    while (sqlreader.Read())
            //    {
            //       var id =  sqlreader.GetInt32(0);
            //        var ten = sqlreader["HoTen"];
            //        var diachi = sqlreader[2];
            //        Console.WriteLine($"{id} - {ten} - {diachi}");
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("Không tim thay du lieu");
            //}

            //command.ExecuteScalar(); - Tra ve 1 gia tri (dong 1, cot 1),thường dùng cho các câu lệnh tính toán

            //command.CommandText = "SELECT count(1) FROM Khachhang WHERE MaBuuDien = @MaBuuDien";
            //var mabuudien = command.Parameters.AddWithValue("@MaBuuDien", 222);
            //mabuudien.Value = 100;
            //var returnValue =  command.ExecuteScalar();
            //Console.WriteLine(returnValue);

            //command.ExecuteNonQuery(); - trả về số lượng row bị ảnh hưởng bởi câu lệnh , thường dùng cho isert, update, delete

            //command.CommandText = "insert into Nhanvien(Ten,Ho) values(@Ten,@Ho)";
            //var ten = command.Parameters.AddWithValue("@Ten","");
            //var ho = command.Parameters.AddWithValue("@Ho", "");
            //ten.Value = "Son";
            //ho.Value = "Ninh";
            //command.CommandText = "update Shippers set Sodienthoai = 0918508367 where ShipperID > @ShipperID";
            //var shipperid = command.Parameters.Add("@ShipperID", 0);
            //shipperid.Value = 1;

            //var returnRows =command.ExecuteNonQuery();
            //Console.WriteLine(returnRows);
            command.CommandText = "getproductinfor";
           command.CommandType = CommandType.StoredProcedure;
            var id = command.Parameters.AddWithValue("@id", 0);
            id.Value = 3;
            var reader =command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                var tensp = reader["TenSanPham"];
                var tendm = reader["TenDanhMuc"];
                Console.WriteLine($"{tensp} - {tendm}");
            }

            connection.Close();
        }
    }
}

