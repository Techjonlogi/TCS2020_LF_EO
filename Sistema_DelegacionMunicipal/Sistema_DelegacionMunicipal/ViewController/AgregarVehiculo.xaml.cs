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
    /// Lógica de interacción para AgregarVehiculo.xaml
    /// </summary>
    public partial class AgregarVehiculo : UserControl
    {
        int idConductorSeleccionado;
        SistemaReportesVehiculosEntities db = new SistemaReportesVehiculosEntities();

        public AgregarVehiculo(int idConductor)
        {
            InitializeComponent();
            idConductorSeleccionado = idConductor;

            txtDuenio.Text = db.Conductor.Where(x => x.idConductor == idConductorSeleccionado).Select(x => x.nombre).FirstOrDefault().ToString();
            txtDuenio.Text += " " + db.Conductor.Where(x => x.idConductor == idConductorSeleccionado).Select(x => x.apellidos).FirstOrDefault().ToString();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            string marca = txt_marca.Text;
            string modelo = txt_modelo.Text;
            string anio = txt_anio.Text;
            string color = txt_color.Text;
            string nombreAseguradora = txt_nomAseguradora.Text;
            string numPoliza = txt_numPoliza.Text;
            string placas = txt_placas.Text;
            int idConductor = idConductorSeleccionado;

            if (string.IsNullOrEmpty(marca) || string.IsNullOrEmpty(modelo) || string.IsNullOrEmpty(anio) || string.IsNullOrEmpty(color) || string.IsNullOrEmpty(placas))
            {
                MessageBox.Show("Campos Vacios...", "Error");
                return;
            }
            if (string.IsNullOrEmpty(nombreAseguradora) || string.IsNullOrEmpty(numPoliza))
            {
                nombreAseguradora = "Ninguna";
                numPoliza = "0";
            }
            try
            {

                using (SistemaReportesVehiculosEntities db = new SistemaReportesVehiculosEntities())
                {
                    Vehiculo vehiculo = new Vehiculo
                    {
                        marca = marca,
                        modelo = modelo,
                        anio = int.Parse(anio),
                        color = color,
                        nombreAseguradora = nombreAseguradora,
                        numPoliza = int.Parse(numPoliza),
                        placas = placas,
                        idConductor = idConductor
                    };
                    db.Vehiculo.Add(vehiculo);
                    db.SaveChanges();
                    MessageBox.Show("Vehículo agregado con éxito");

                    this.Visibility = Visibility.Collapsed;
                }
            }
            catch
            {
                MessageBox.Show("Error");

            }
        }

        private void cb_dueño_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
