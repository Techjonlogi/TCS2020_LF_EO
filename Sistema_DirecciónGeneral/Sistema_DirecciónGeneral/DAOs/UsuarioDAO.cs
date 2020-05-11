using Sistema_DirecciónGeneral.Clases;
using Sistema_DirecciónGeneral.DataBase;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_DirecciónGeneral.DAOs
{
    class UsuarioDAO
    {
        public static Usuario GetLogin(String user, String contrasenia)
        {
            Usuario userGeneral = null;
            //SqlConnection conn = null;
            Connection dbConnection = new Connection();
            using (SqlConnection connection = dbConnection.GetConnection())
            {
                try
                {
                    connection.Open();
                }
                catch (SqlException)
                {
                    resultado = AddResult.SQLFail;
                    return resultado;
                }

                using (SqlCommand command = new SqlCommand("INSERT INTO dbo.Usuarios VALUES(@nombre, @correo, @registro, @tipo, @estatus, @usuario, @contraseña)", connection))
                {
                    command.Parameters.Add(new SqlParameter("@nombre", usuario.Name));
                    command.Parameters.Add(new SqlParameter("@correo", usuario.Email));
                    command.Parameters.Add(new SqlParameter("@registro", usuario.RegisterDate));
                    command.Parameters.Add(new SqlParameter("@tipo", usuario.UserType));
                    command.Parameters.Add(new SqlParameter("@estatus", ""));
                    command.Parameters.Add(new SqlParameter("@usuario", usuario.UserName));
                    command.Parameters.Add(new SqlParameter("@contraseña", PassHash(usuario.Password)));
                    command.ExecuteNonQuery();
                    resultado = AddResult.Success;
                }
                connection.Close();
            }
            try
            {
                mmand command;
                SqlDataReader rd;
                if (conn != null)
                {
                    String query = String.Format("SELECT " +
                        "x.idUsuario," +
                        "x.usuario," +
                        "x.contrasenia," +                        
                        "x.nombre," +
                        "x.apellidos," +
                        "x.cargo," +
                        "x.idDelegación " +
                        "FROM dbo.Usuario x " +
                        "WHERE x.usuario = '{0}' AND x.contrasenia = '{1}'", user, contrasenia);
                    Console.WriteLine(query);
                    command = new SqlCommand(query, conn);
                    rd = command.ExecuteReader();
                    while (rd.Read())
                    {
                        userGeneral = new Usuario
                        {
                            Idusuario = (!rd.IsDBNull(0)) ? rd.GetInt32(0) : 0,
                            Nombreuser = (!rd.IsDBNull(1)) ? rd.GetString(1) : "",
                            Contrasenia = (!rd.IsDBNull(2)) ? rd.GetString(2) : ""
                        };
                    }
                    rd.Close();
                    command.Dispose();
                    Console.WriteLine(userGeneral);
                }
            }
            
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return userGeneral;
        }
    }
}
