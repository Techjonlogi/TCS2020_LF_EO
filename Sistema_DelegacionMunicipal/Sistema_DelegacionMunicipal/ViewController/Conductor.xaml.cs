using MaterialDesignThemes.Wpf;
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
    /// Lógica de interacción para Conductor.xaml
    /// </summary>
    public partial class Conductor : UserControl
    {

        public Conductor()
        {
            InitializeComponent();
            GridConductor.Children.Clear();

            btnCancelar.Visibility = Visibility.Hidden;
            btnAgregarConductor.Visibility = Visibility.Hidden;
            btnAgregarVehiculo.Visibility = Visibility.Hidden;
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnNuevoConductor_Click(object sender, RoutedEventArgs e)
        {
            if (GridConductor.Children.Count < 1)
            {
                GridConductor.Children.Clear();
                GridConductor.Children.Add(new AgregarConductor());

                btnNuevoConductor.Visibility = Visibility.Hidden;
                btnNuevoVehiculo.Visibility = Visibility.Hidden;

                btnCancelar.Visibility = Visibility.Visible;
                btnAgregarConductor.Visibility = Visibility.Visible;
            }
        }


        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (GridConductor.Children.Count > 0)
            {
                GridConductor.Children.RemoveAt(0);

                btnNuevoConductor.Visibility = Visibility.Visible;
                btnNuevoVehiculo.Visibility = Visibility.Visible;

                btnCancelar.Visibility = Visibility.Hidden;
                btnAgregarConductor.Visibility = Visibility.Hidden;
                btnAgregarVehiculo.Visibility = Visibility.Hidden;
            }
                
        }

        private void btnNuevoVehiculo_Click(object sender, RoutedEventArgs e)
        {
            if (GridConductor.Children.Count < 1)
            {
                GridConductor.Children.Clear();
                GridConductor.Children.Add(new AgregarVehiculo());

                btnNuevoConductor.Visibility = Visibility.Hidden;
                btnNuevoVehiculo.Visibility = Visibility.Hidden;

                btnCancelar.Visibility = Visibility.Visible;
                btnAgregarVehiculo.Visibility = Visibility.Visible;
            }
        }

    }
}
