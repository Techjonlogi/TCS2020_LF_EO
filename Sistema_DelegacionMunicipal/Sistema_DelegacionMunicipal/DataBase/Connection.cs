using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestionEgresados.DataBase
{
    public class ConnectionUtils
    {
        private static String SERVER = "localhost";
        private static String PORT = "1433";
        private static String DATABASE = "SistemaReportesVehiculos";
        private static String USER = "sa";
        private static String PASSWORD = "1050g2";

        public static SqlConnection getConnection()
        {
            SqlConnection conn = null;
            try
            {
                String urlconn = String.Format("Data Source={0},{1};" +
                                               "Network Library=DBMSSOCN;" +
                                               "Initial Catalog={2};" +
                                               "User ID={3};" +
                                               "Password={4};",
                                               SERVER, PORT, DATABASE, USER, PASSWORD);
                conn = new SqlConnection(urlconn);
                conn.Open();
                return conn;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("ERROR EN LA CONEXIÓN");
            }
            return conn;
        }

    }
}
