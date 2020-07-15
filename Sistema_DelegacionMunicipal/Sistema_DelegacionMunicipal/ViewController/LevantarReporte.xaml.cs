using Microsoft.Win32;
using Sistema_DelegacionMunicipal.Classes;
using Sistema_DelegacionMunicipal.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Sistema_DelegacionMunicipal.ViewController
{
    /// <summary>
    /// Lógica de interacción para LevantarReporte.xaml
    /// </summary>
    public partial class LevantarReporte : UserControl
    {
        SistemaReportesVehiculosEntities db;
        int idConductorSeleccionado = 0;
        int idImagen = 0;

        List<Involucrado> listaInvolucrados = new List<Involucrado>();
        List<string> placasVehiculosInvolucrados = new List<string>();

        List<string> listNombresConductores = new List<string>();
        List<string> listApellidosConductores = new List<string>();
        List<string> listMarcasVehiculos = new List<string>();
        List<string> listModelosVehiculos = new List<string>();
        List<int> listAniosVehiculos = new List<int>();
        List<string> listPlacasVehiculos = new List<string>();

        List<string> archivoImagen = new List<string>();
        List<Stream> streamImagen = new List<Stream>();


        int idDelegacionEmisor = 0;

        Socket socketCliente;
        Chat chat;
        public LevantarReporte(int idDelegacionEmisor, Socket socketCliente, Chat chat)
        {
            this.idDelegacionEmisor = idDelegacionEmisor;
            this.socketCliente = socketCliente;
            this.chat = chat;
            InitializeComponent();
            
        }

        public LevantarReporte()
        {

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
            if (list.Count() < 1 && cbConductores.SelectedIndex > 0)
                labelMensajeVehiculos.Content = "*Este conductor no tiene vehículos";

            else
                labelMensajeVehiculos.Content = "";

            cbVehiculos.ItemsSource = list;

            listMarcasVehiculos = db.Vehiculo.Where(x => x.idConductor == idConductorSeleccionado).Select(x => x.marca).ToList();
            listModelosVehiculos = db.Vehiculo.Where(x => x.idConductor == idConductorSeleccionado).Select(x => x.modelo).ToList();
            listAniosVehiculos = db.Vehiculo.Where(x => x.idConductor == idConductorSeleccionado).Select(x => x.anio).ToList();

            listPlacasVehiculos.Clear();
            listPlacasVehiculos = db.Vehiculo.Where(x => x.idConductor == idConductorSeleccionado).Select(x => x.placas).ToList();
        }

        private void cbConductores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            idConductorSeleccionado = cbConductores.SelectedIndex + 1;
            labelMensajeConductores.Content = "";
            labelMensajeVehiculos.Content = "";
            llenarComboVehiculos();
        }


        private void btnAñadirInvolucrado_Click(object sender, RoutedEventArgs e)
        {
            if (cbConductores.SelectedItem != null && cbVehiculos.SelectedItem != null)
            {
                labelMensajeAñadirInvolucrado.Content = "";

                string nombre = listNombresConductores[cbConductores.SelectedIndex] + " " + listApellidosConductores[cbConductores.SelectedIndex];
                string vehiculo = listMarcasVehiculos[cbVehiculos.SelectedIndex]
                                 + " " + listModelosVehiculos[cbVehiculos.SelectedIndex] + " "
                                 + listAniosVehiculos[cbVehiculos.SelectedIndex];


                //Validar que no se haya añadido el vehiculo
                for (int i  = 0; i < listaInvolucrados.Count; i++)
                {
                    if (vehiculo == listaInvolucrados[i].nombreVehiculo)
                    {
                        labelMensajeVehiculos.Content = "*Este vehículo ya ha sido añadido";
                        return;
                    }
                }

                placasVehiculosInvolucrados.Add(listPlacasVehiculos[cbVehiculos.SelectedIndex]);
                listaInvolucrados.Add(new Involucrado(nombre, vehiculo));
                dataGridInvolucrados.ItemsSource = null;
                dataGridInvolucrados.ItemsSource = listaInvolucrados;

                cbConductores.SelectedIndex = -1;
                cbVehiculos.ItemsSource = null;
            }

            else
            {
                labelMensajeAñadirInvolucrado.Content = "*Debes seleccionar un conductor y un vehículo para añadir";
            }

        }

        private void cbConductores_DropDownOpened(object sender, EventArgs e)
        {
            llenarComboConductores();
        }


        private void btnSeleccionarFotografias_Click(object sender, RoutedEventArgs e)
        {
            

            OpenFileDialog op = new OpenFileDialog();

            op.Multiselect = true;
            op.Title = "Selecciona de 3 a 8 imágenes";
            op.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            if (op.ShowDialog() == true)
            {
                archivoImagen = op.FileNames.ToList();
                streamImagen.Clear();
                

                for (int i = 0; i < archivoImagen.Count; i++)
                {
                    streamImagen.Add(op.OpenFiles()[i]);

                    switch (i)
                    {
                        case 0:
                            imagen1.Source = new BitmapImage(new Uri(archivoImagen[i]));
                            imagen2.Source = null;
                            imagen3.Source = null;
                            imagen4.Source = null;
                            imagen5.Source = null;
                            imagen6.Source = null;
                            imagen7.Source = null;
                            imagen8.Source = null;
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



        private void BtnLevantarReporte_Click(object sender, RoutedEventArgs e)
        {
            //VALIDAR QUE SE HAYAN LLENADO LOS CAMPOS DEL REPORTE

            if (listaInvolucrados.Count > 0 && txtDireccion.Text.Trim() != null && archivoImagen.Count > 2)
            {
                //GUARDAR LAS IMÁGENES EN LA BASE

                Imagenes img = new Imagenes();

                using (SistemaReportesVehiculosEntities db = new SistemaReportesVehiculosEntities())
                {
                    for (int i = 0; i < streamImagen.Count(); i++)
                    {
                        byte[] file = null;
                        using (MemoryStream ms = new MemoryStream())
                        {
                            streamImagen[i].CopyTo(ms);
                            file = ms.ToArray();
                        }

                        switch (i)
                        {
                            case 0:
                                img.imagen1 = file;
                                db.Imagenes.Add(img);
                                break;
                            case 1:
                                img.imagen2 = file;
                                db.Imagenes.Add(img);
                                break;
                            case 2:
                                img.imagen3 = file;
                                db.Imagenes.Add(img);
                                break;
                            case 3:
                                img.imagen4 = file;
                                db.Imagenes.Add(img);
                                break;
                            case 4:
                                img.imagen5 = file;
                                db.Imagenes.Add(img);
                                break;
                            case 5:
                                img.imagen6 = file;
                                db.Imagenes.Add(img);
                                break;
                            case 6:
                                img.imagen7 = file;
                                db.Imagenes.Add(img);
                                break;
                            case 7:
                                img.imagen8 = file;
                                db.Imagenes.Add(img);
                                break;
                        }
                    }
                    db.SaveChanges();
                }


                //GUARDAR TODO EN REPORTE DE LA BASE

                int idReporte = 0;
                using (SistemaReportesVehiculosEntities db = new SistemaReportesVehiculosEntities())
                {
                    //Buscar id de las imagenes
                    try
                    {
                        idImagen = db.Imagenes.Select(x => x.idImagenes).Count();
                    }
                    catch
                    {
                        MessageBox.Show("Error, no se pudo conseguir idImagen y no se guardo el reporte.");
                        return;
                    }

                    //Guardar reporte
                    try
                    {
                        Reporte reporte = new Reporte();
                        reporte.direccion = txtDireccion.Text;
                        reporte.numCarrosInvolucrados = listaInvolucrados.Count();
                        reporte.idDelegación = idDelegacionEmisor;
                        reporte.idImagenes = idImagen;

                        db.Reporte.Add(reporte);
                        db.SaveChanges();
                    }
                    catch
                    {
                        MessageBox.Show("Error, no se pudo guardar el reporte");
                        return;
                    }
                    //Buscar id del reporte
                    try
                    {
                        idReporte = db.Reporte.Select(x => x.idReporte).Count();
                    }
                    catch
                    {
                        MessageBox.Show("Error, no se pudo conseguir idReporte.");
                        return;
                    }

                    //Guardar matriculas de vehiculos de reporte
                    try
                    {
                        VehiculoReporte vr = new VehiculoReporte();

                        for (int i = 0; i < idReporte; i++)
                        {
                            vr.idReporte = idReporte;
                            db.VehiculoReporte.Add(vr);
                            vr.placas = placasVehiculosInvolucrados[i];
                            db.VehiculoReporte.Add(vr);
                            db.SaveChanges();
                        }
                    }
                    catch
                    {

                    }
                }

                //Enviar notificación de reporte a Servidor
                InformacionReporteEnvio infoNotificacionReporte = new InformacionReporteEnvio(false, true, " ");
                EnviarNotificacionReporte(infoNotificacionReporte);
            }
            else
            {
                MessageBox.Show("Los campos no pueden estar vacíos, debe haber al menos un vehículo añadido y al menos 3 imágenes.", "Error al guardar");
            }

            
        }

        private void EnviarNotificacionReporte(InformacionReporteEnvio infoReporte)
        {
            string infoReporteSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(infoReporte).ToString();
            chat.EnviarReporte(infoReporteSerializado);
            limpiarCamposReporte();

            MessageBox.Show("Reporte guardado correctamente");
        }

        private void limpiarCamposReporte()
        {
            cbConductores.SelectedIndex = -1;
            cbVehiculos.ItemsSource = null;
            txtDireccion.Text = "";
            archivoImagen.Clear();
            dataGridInvolucrados.ItemsSource = null;
            listaInvolucrados.Clear();
            imagen1.Source = null;
            imagen2.Source = null;
            imagen3.Source = null;
            imagen4.Source = null;
            imagen5.Source = null;
            imagen6.Source = null;
            imagen7.Source = null;
            imagen8.Source = null;
        }

        private void BtnLimpiarCampos_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("¿Desea limpiar todos los campos?", "Confirmación", System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                limpiarCamposReporte();
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
