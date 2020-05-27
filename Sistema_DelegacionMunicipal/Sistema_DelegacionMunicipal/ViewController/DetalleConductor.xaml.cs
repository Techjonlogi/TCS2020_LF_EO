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
    /// Lógica de interacción para DetalleConductor.xaml
    /// </summary>
    public partial class DetalleConductor : UserControl
    {

        public DetalleConductor(int idConductorSeleccionado)
        {
            InitializeComponent();
            llenarTablaVehiculos(idConductorSeleccionado);
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        public void llenarTablaVehiculos(int idConductorSeleccionado)
        {
            Modelo.SistemaReportesVehiculosEntities db = new Modelo.SistemaReportesVehiculosEntities();
            dataGridVehiculos.ItemsSource = db.Vehiculo.Where(x => x.idConductor == idConductorSeleccionado).ToList();
        }

        
        private void dataGridConductores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void btnNuevoVehiculo_Click(object sender, RoutedEventArgs e)
        {
            AgregarVehiculo agregarVehiculo = new AgregarVehiculo();
            gridVehiculo.Children.Clear();
            gridVehiculo.Children.Add(agregarVehiculo);
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }

}
