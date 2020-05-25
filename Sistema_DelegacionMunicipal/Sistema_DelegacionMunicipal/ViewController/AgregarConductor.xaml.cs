using Sistema_DelegacionMunicipal.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sistema_DelegacionMunicipal.ViewController
{
    /// <summary>
    /// Lógica de interacción para AgregarConductor.xaml
    /// </summary>
    public partial class AgregarConductor : UserControl
    {
        public AgregarConductor()
        {
            InitializeComponent();
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            string nombre = txtNombre.Text;
            string apellidos = txtApellidos.Text;
            string fechaNacimiento = txtNacimiento.Text;
            string numLicencia = txtLicencia.Text;
            string telefono = txtTelefono.Text;

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellidos) || string.IsNullOrEmpty(fechaNacimiento) || string.IsNullOrEmpty(numLicencia) || string.IsNullOrEmpty(telefono))
            {
                MessageBox.Show("Usuario y/o password Vacios...", "Error");
                return;
            }
            try
            {
                //IQueryable query;
                using (SistemaReportesVehiculosEntities db = new SistemaReportesVehiculosEntities())
                {
                    Conductor conductor = new Conductor
                    {
                        nombre = nombre,
                        apellidos = apellidos,
                        fechaNacimiento = fechaNacimiento,
                        numLicencia = numLicencia,
                        telefono = telefono
                    };
                    db.Conductor.Add(conductor);
                    db.SaveChanges();
                    MessageBox.Show("Agregado con éxito");

                    this.Visibility = Visibility.Collapsed;
                }
            }
            catch
            {
                MessageBox.Show("Error");

            }

        }
    }
}
