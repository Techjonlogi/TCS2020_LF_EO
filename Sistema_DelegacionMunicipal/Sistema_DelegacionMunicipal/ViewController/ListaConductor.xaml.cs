using MaterialDesignThemes.Wpf;
using Sistema_DelegacionMunicipal.Interface;
using Sistema_DelegacionMunicipal.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
    /// Lógica de interacción para Conductor.xaml
    /// </summary>
    public partial class ListaConductor : UserControl, IConductor
    {

        List<string> listaConductores = new List<string>();

        public ListaConductor()
        {
            InitializeComponent();
            GridConductor.Children.Clear();

            llenarTablaConductores();

        }

        public async Task llenarTablaConductores()
        {
            using (var db = new SistemaReportesVehiculosEntities())
            {
                dataGridConductores.ItemsSource = await db.Conductor.ToListAsync();
            }
        }


        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    

        private void BtnNuevoConductor_Click(object sender, RoutedEventArgs e)
        {
            AgregarConductor agregarConductor = new AgregarConductor(0, this);
            GridConductor.Children.Clear();
            GridConductor.Children.Add(agregarConductor);
        }


        /*
        private void btnNuevoVehiculo_Click(object sender, RoutedEventArgs e)
        {
            AgregarVehiculo agregarVehiculo = new AgregarVehiculo(idConductor, this);
            GridConductor.Children.Clear();
            GridConductor.Children.Add(agregarVehiculo);

        }
        */

        private void btnDetalleConductor_Click(object sender, RoutedEventArgs e)
        {
            int idConductor = (int)((Button)sender).CommandParameter;

            DetalleConductor detalleConductor = new DetalleConductor(idConductor, this);
            GridConductor.Children.Clear();
            GridConductor.Children.Add(detalleConductor);
            llenarTablaConductores();
        }

        public void Actualizar(int idConductor)
        {
            llenarTablaConductores();
        }

        private void Button_Modificar_Click(object sender, RoutedEventArgs e)
        {
            int idConductor = (int)((Button)sender).CommandParameter;

            AgregarConductor agregarConductor = new AgregarConductor(idConductor, this);
            GridConductor.Children.Clear();
            GridConductor.Children.Add(agregarConductor);

            llenarTablaConductores();
        }
    }
}
