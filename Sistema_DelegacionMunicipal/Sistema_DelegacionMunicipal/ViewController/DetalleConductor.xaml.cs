﻿using Sistema_DelegacionMunicipal.Modelo;
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

        Modelo.SistemaReportesVehiculosEntities db = new Modelo.SistemaReportesVehiculosEntities();
        int idConductorSeleccionado = 0;

        public DetalleConductor(int idConductorSeleccionado_)
        {
            InitializeComponent();
            this.idConductorSeleccionado = idConductorSeleccionado_;
            llenarTablaVehiculos();
            llenarInformacion();
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        public void llenarTablaVehiculos()
        {
            dataGridVehiculos.ItemsSource = db.Vehiculo.Where(x => x.idConductor == idConductorSeleccionado).ToList();
        }

        public void llenarInformacion()
        {
            txtNombre.Text = db.Conductor.Where(x => x.idConductor == idConductorSeleccionado).Select(x => x.nombre).FirstOrDefault().ToString() +
                             " " + db.Conductor.Where(x => x.idConductor == idConductorSeleccionado).Select(x => x.apellidos).FirstOrDefault().ToString();

            txtNacimiento.Text = db.Conductor.Where(x => x.idConductor == idConductorSeleccionado).Select(x => x.fechaNacimiento).FirstOrDefault().ToString();
            txtLicencia.Text = db.Conductor.Where(x => x.idConductor == idConductorSeleccionado).Select(x => x.numLicencia).FirstOrDefault().ToString();
            txtTelefono.Text = db.Conductor.Where(x => x.idConductor == idConductorSeleccionado).Select(x => x.telefono).FirstOrDefault().ToString();
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
