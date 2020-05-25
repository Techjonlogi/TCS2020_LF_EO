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
        public AgregarVehiculo()
        {
            InitializeComponent();
            CargarConductores();
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
            //int idCondutor = cb_dueño.SelectedIndex;

            if (string.IsNullOrEmpty(marca) || string.IsNullOrEmpty(modelo) || string.IsNullOrEmpty(anio) || string.IsNullOrEmpty(color) || string.IsNullOrEmpty(numPoliza) || string.IsNullOrEmpty(nombreAseguradora) || string.IsNullOrEmpty(placas))
            {
                MessageBox.Show("Usuario y/o password Vacios...", "Error");
                return;
            }
            try
            {
                //IQueryable query;
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
                        placas = placas
                    };
                    db.Vehiculo.Add(vehiculo);
                    db.SaveChanges();
                    MessageBox.Show("Agregado con éxito");
                }
            }
            catch
            {
                MessageBox.Show("Error");

            }
        }

        private void CargarConductores()
        {
            SistemaReportesVehiculosEntities db = new SistemaReportesVehiculosEntities();
            var list = db.Conductor.ToList();
            if (list.Count() > 0)
            {
                cb_dueño.ItemsSource = list;
                cb_dueño.DisplayMemberPath = "nombre";
                cb_dueño.SelectedValuePath = "idConductor";
            }
        }
    }
}
