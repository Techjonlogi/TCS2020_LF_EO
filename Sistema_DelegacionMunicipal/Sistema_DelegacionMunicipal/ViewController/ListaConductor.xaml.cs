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
    public partial class ListaConductor : UserControl
    {

        public ListaConductor()
        {
            InitializeComponent();
            GridConductor.Children.Clear();
            
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
            DataGrid dataGrid = sender as DataGrid;
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            DataGridCell RowColumn = dataGrid.Columns[1].GetCellContent(row).Parent as DataGridCell;
            string CellValue = ((TextBlock)RowColumn.Content).Text;
            //matriculaSeleccionada = CellValue;
        }

        private void btnDetalleConductor_Click(object sender, RoutedEventArgs e)
        {
            DetalleConductor detalleConductor = new DetalleConductor();
            gridDetallesConductor.Children.Clear();
            gridDetallesConductor.Children.Add(detalleConductor);
        }
    }
}
