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
    /// Lógica de interacción para HistorialReportes.xaml
    /// </summary>
    public partial class HistorialReportes : UserControl
    {
        public HistorialReportes()
        {
            InitializeComponent();
            btnVolver.Visibility = Visibility.Hidden;
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }



        private void BtnVerDictamen_Click(object sender, RoutedEventArgs e)
        {
            if (gridHistorial.Children.Count < 1)
            {
                gridHistorial.Children.Clear();
                gridHistorial.Children.Add(new Dictamen());

                btnVisualizarReporte.Visibility = Visibility.Hidden;
                btnVerDictamen.Visibility = Visibility.Hidden;

                btnVolver.Visibility = Visibility.Visible;
            }
        }


        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            if (gridHistorial.Children.Count > 0)
            {
                gridHistorial.Children.RemoveAt(0);

                btnVisualizarReporte.Visibility = Visibility.Visible;
                btnVerDictamen.Visibility = Visibility.Visible;

                btnVolver.Visibility = Visibility.Hidden;
            }

        }
    }
}
