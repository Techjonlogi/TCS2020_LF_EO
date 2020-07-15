using Sistema_DelegacionMunicipal.Interface;
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
        int idConductorSeleccionado = 0;
        IVehiculo itActualizar;
        public AgregarVehiculo(int idConductorSeleccionado, IVehiculo itActualizar)
        {
            InitializeComponent();
            this.idConductorSeleccionado = idConductorSeleccionado;

            
            this.itActualizar = itActualizar;
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
                        idConductor = idConductorSeleccionado
                    };
                    db.Vehiculo.Add(vehiculo);
                    db.SaveChanges();
                    MessageBox.Show("Vehículo agregado con éxito");

                }
                this.Visibility = Visibility.Collapsed;
                this.itActualizar.Actualizar(idConductorSeleccionado);

            }
            catch
            {
                MessageBox.Show("Error");

            }
        }

        
    }
}
