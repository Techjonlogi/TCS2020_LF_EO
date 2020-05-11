using GestionEgresados.DataBase;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sistema_DelegacionMunicipal.DAOs
{
    class ConductorDAO
    {

        public void AgregarConductor(string nombre, string apellidos, string numLicencia, string telefono)
        {
            SqlConnection conn = null;
                //getCantiadEgresados() + 1;

            try
            {
                conn = ConnectionUtils.getConnection();
                SqlCommand command;
                if (conn != null)
                {
                    String query = String.Format("INSERT INTO dbo.Conductor " +
                        "(nombre, apellidos, numLicencia, telefono)	" +
                        "VALUES('{0}', '{1}', '{2}', '{3}'); "
                        , nombre, apellidos, numLicencia, telefono);

                    Console.WriteLine(query);
                    command = new SqlCommand(query, conn);
                    command.ExecuteNonQuery();
                    command.Dispose();

                    MessageBox.Show("El conductor ha sido registrado.");

                }

            }
            //Cambiar las excepciones.
            catch (Exception ex)
            {
                MessageBox.Show("No se han podido realizar el registro.");
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }



    }
}
