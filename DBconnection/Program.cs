using System;
using System.Configuration;
using System.Data.SqlClient;

namespace DatabaseConnection
{
    public string ConnectionString(){
        var info = new SqlConnectionStringBuilder(){
            DataSource = "SQLServer/localhost",
            IntegratedSecurity = false,
            UserID = "root",
            Password = "pass"
        };
        return info.ToString();
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Database connection test!");
            string connection_info = ConnectionString();
            using(var conn = new Sqlconnection(connection_info))
            {
                conn.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try{
                        var cmd = conn.CreateCommand();
                        cmd.CommandText = "SELECT * FROM Employees";
                        cmd.ExecuteNonQuery();
                    }catch(Exception e){
                        transaction.Rollback();
                        Console.WriteLine(exception.Message);
                        throw;
                    }
                }
            }
        }
    }
}

