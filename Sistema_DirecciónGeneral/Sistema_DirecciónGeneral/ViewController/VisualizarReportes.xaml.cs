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
using System.Windows.Shapes;

namespace Sistema_DirecciónGeneral.ViewController
{
    /// <summary>
    /// Lógica de interacción para VisualizarReportes.xaml
    /// </summary>
    public partial class VisualizarReportes : UserControl
    {
        public VisualizarReportes()
        {
            InitializeComponent();
            btnVolver.Visibility = Visibility.Hidden;
            btnFinalizarDictamen.Visibility = Visibility.Hidden;
        }


        private void Button_Salir(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnDictaminar_Click(object sender, RoutedEventArgs e)
        {
            if (gridDictamen.Children.Count < 1)
            {
                gridDictamen.Children.Clear();
                gridDictamen.Children.Add(new DictaminarReporte());

                btnVolver.Visibility = Visibility.Visible;
                btnVolver.Content = "Cancelar";
                btnFinalizarDictamen.Visibility = Visibility.Visible;

                btnDictaminarReporte.Visibility = Visibility.Hidden;
                btnVerDetalles.Visibility = Visibility.Hidden;
            }

        }


        private void BtnVerDetalles_Click(object sender, RoutedEventArgs e)
        {
            if (gridDetalles.Children.Count < 1)
            {
                gridDetalles.Children.Clear();
                gridDetalles.Children.Add(new DetallesReporte());

                btnVolver.Visibility = Visibility.Visible;
                btnVolver.Content = "Volver";

                btnDictaminarReporte.Visibility = Visibility.Hidden;
                btnVerDetalles.Visibility = Visibility.Hidden;
            }
        }

        private void BtnVolver_Click(object sender, RoutedEventArgs e)
        {
            if (gridDetalles.Children.Count > 0)
            {
                btnVolver.Visibility = Visibility.Hidden;

                btnDictaminarReporte.Visibility = Visibility.Visible;
                btnVerDetalles.Visibility = Visibility.Visible;

                gridDetalles.Children.RemoveAt(0);
            }

            if (gridDictamen.Children.Count > 0)
            {
                btnVolver.Visibility = Visibility.Hidden;
                btnFinalizarDictamen.Visibility = Visibility.Hidden;

                btnDictaminarReporte.Visibility = Visibility.Visible;
                btnVerDetalles.Visibility = Visibility.Visible;

                gridDictamen.Children.RemoveAt(0);
            }
        }
    }
}
