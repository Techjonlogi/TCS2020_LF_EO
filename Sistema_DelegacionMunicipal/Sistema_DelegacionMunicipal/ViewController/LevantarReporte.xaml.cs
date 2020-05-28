using Microsoft.Win32;
using Sistema_DelegacionMunicipal.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
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
    /// Lógica de interacción para LevantarReporte.xaml
    /// </summary>
    public partial class LevantarReporte : UserControl
    {
        SistemaReportesVehiculosEntities db;
        int idConductorSeleccionado = 0;

        List<Involucrado> listaInvolucrados = new List<Involucrado>();
        List<int> idVehiculosInvolucradosSeleccionados = new List<int>();

        List<string> listNombresConductores = new List<string>();
        List<string> listApellidosConductores = new List<string>();
        List<string> listMarcasVehiculos = new List<string>();
        List<string> listModelosVehiculos = new List<string>();
        List<int> listAniosVehiculos = new List<int>();

        public LevantarReporte()
        {
            InitializeComponent();

        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        public void llenarComboConductores()
        {
            db = new SistemaReportesVehiculosEntities();

            var list = db.Conductor.ToList();
            if (list.Count() < 1)
                labelMensajeConductores.Content = "*No hay conductores registrados";
            else
                labelMensajeConductores.Content = "";

            cbConductores.ItemsSource = list;

            listNombresConductores = db.Conductor.Select(x => x.nombre).ToList();
            listApellidosConductores = db.Conductor.Select(x => x.apellidos).ToList();
            

        }

        public void llenarComboVehiculos()
        {
            db = new SistemaReportesVehiculosEntities();

            var list = db.Vehiculo.Where(x => x.idConductor == idConductorSeleccionado).ToList();
            if (list.Count() < 1)
                labelMensajeVehiculos.Content = "*Este conductor no tiene vehículos";

            else
                labelMensajeVehiculos.Content = "";

            cbVehiculos.ItemsSource = list;

            listMarcasVehiculos = db.Vehiculo.Where(x => x.idConductor == idConductorSeleccionado).Select(x => x.marca).ToList();
            listModelosVehiculos = db.Vehiculo.Where(x => x.idConductor == idConductorSeleccionado).Select(x => x.modelo).ToList();
            listAniosVehiculos = db.Vehiculo.Where(x => x.idConductor == idConductorSeleccionado).Select(x => x.anio).ToList();


        }

        private void cbConductores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            idConductorSeleccionado = cbConductores.SelectedIndex + 1;
            llenarComboVehiculos();
        }


        private void btnAñadirInvolucrado_Click(object sender, RoutedEventArgs e)
        {
            if (cbConductores.Items.Count > 0 && cbVehiculos.Items.Count > 0)
            {
                string nombre = listNombresConductores[cbConductores.SelectedIndex] + " " + listApellidosConductores[cbConductores.SelectedIndex];
                string vehiculo = listMarcasVehiculos[cbVehiculos.SelectedIndex]
                                 + " " + listModelosVehiculos[cbVehiculos.SelectedIndex] + " "
                                 + listAniosVehiculos[cbVehiculos.SelectedIndex];

                listaInvolucrados.Add(new Involucrado(nombre, vehiculo));
                dataGridInvolucrados.ItemsSource = null;
                dataGridInvolucrados.ItemsSource = listaInvolucrados;
            }

        }

        private void cbConductores_DropDownOpened(object sender, EventArgs e)
        {
            llenarComboConductores();
        }

        private void btnSeleccionarFotografias_Click(object sender, RoutedEventArgs e)
        {
            List<string> archivoImagen = new List<string>();

            OpenFileDialog op = new OpenFileDialog();

            op.Multiselect = true;
            op.Title = "Selecciona de 3 a 8 imágenes";
            op.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            if (op.ShowDialog() == true)
            {
                archivoImagen = op.FileNames.ToList();

                for(int i = 0; i < archivoImagen.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            imagen1.Source = new BitmapImage(new Uri(archivoImagen[i]));
                            break;
                        case 1:
                            imagen2.Source = new BitmapImage(new Uri(archivoImagen[i]));
                            break;
                        case 2:
                            imagen3.Source = new BitmapImage(new Uri(archivoImagen[i]));
                            break;
                        case 3:
                            imagen4.Source = new BitmapImage(new Uri(archivoImagen[i]));
                            break;
                        case 4:
                            imagen5.Source = new BitmapImage(new Uri(archivoImagen[i]));
                            break;
                        case 5:
                            imagen6.Source = new BitmapImage(new Uri(archivoImagen[i]));
                            break;
                        case 6:
                            imagen7.Source = new BitmapImage(new Uri(archivoImagen[i]));
                            break;
                        case 7:
                            imagen8.Source = new BitmapImage(new Uri(archivoImagen[i]));
                            break;

                    }
                    
                }
       
            }
        }
    }



    public class Involucrado
    {
        public string nombreConductor { get; set; }
        public string nombreVehiculo { get; set; }

        public Involucrado(string nombreConductor_, string nombreVehiculo_)
        {
            this.nombreConductor = nombreConductor_;
            this.nombreVehiculo = nombreVehiculo_;
        }

    }


}
