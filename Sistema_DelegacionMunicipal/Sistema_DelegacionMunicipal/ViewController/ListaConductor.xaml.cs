using MaterialDesignThemes.Wpf;
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
    public partial class ListaConductor : UserControl
    {

        int conductorSeleccionado = 0;
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
            AgregarConductor agregarConductor = new AgregarConductor();
            GridConductor.Children.Clear();
            GridConductor.Children.Add(agregarConductor);
        }



        private void btnNuevoVehiculo_Click(object sender, RoutedEventArgs e)
        {
            AgregarVehiculo agregarVehiculo = new AgregarVehiculo();
            GridConductor.Children.Clear();
            GridConductor.Children.Add(agregarVehiculo);

        }

        private void dataGridConductores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*
            DataGrid dataGrid = sender as DataGrid;
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            DataGridCell RowColumn = dataGrid.Columns[1].GetCellContent(row).Parent as DataGridCell;
            int CellValue = (int)RowColumn;*/

            conductorSeleccionado = dataGridConductores.SelectedIndex + 1;
        }

        private void btnDetalleConductor_Click(object sender, RoutedEventArgs e)
        {
            DetalleConductor detalleConductor = new DetalleConductor(conductorSeleccionado);
            gridDetallesConductor.Children.Clear();
            gridDetallesConductor.Children.Add(detalleConductor);
            llenarTablaConductores();
        }
    }
}
