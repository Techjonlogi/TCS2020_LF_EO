using Sistema_DirecciónGeneral.Interfaces;
using Sistema_DirecciónGeneral.Modelo;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Lógica de interacción para DetallesReporte.xaml
    /// </summary>
    public partial class DetallesReporte : UserControl
    {
        IReporte itActualizar;
        int idReporte = 0;
        int idUsuario = 0;
        string folio = "";
        string placasRespon = "";
        
        public DetallesReporte(int idUsuario, int idReporte, string folio, string placasRespon, IReporte itActualizar)
        {
            InitializeComponent();
            this.idReporte = idReporte;
            this.folio = folio;
            this.placasRespon = placasRespon;
            this.idUsuario = idUsuario;
            if (this.idReporte > 0)
            {
                LlenarDetallesReportes();
            }
            this.itActualizar = itActualizar;
        }

        private void LlenarDetallesReportes()
        {
            using (SistemaReportesVehiculosEntities db = new SistemaReportesVehiculosEntities())
            {
                Reporte reporte = db.Reporte.Find(idReporte);
                Delegacion delegacion = db.Delegacion.Find(reporte.idDelegación);
                Dictamen dictamen = db.Dictamen.Find(folio);
                Vehiculo vehiculo = db.Vehiculo.Find(dictamen.responsable);
                Usuario usuario = db.Usuario.Find(idUsuario);
                if (folio.Length == 12)
                {
                    Conductor conductor = db.Conductor.Find(vehiculo.idConductor);
                    txt_responsable.Content = conductor.nombre + " " + conductor.apellidos;
                    txt_vehiculo.Content = vehiculo.marca + " " + vehiculo.modelo + " " + vehiculo.placas;

                }
                txt_delegacion.Content = delegacion.nombre;
                if (folio.Length != 12)
                {
                    txt_estatus.Content = "Sin dictamen";
                }
                else
                {
                    txt_estatus.Content = "Dictaminado";
                }

                txt_direccion.Text = reporte.direccion;
                txt_descripcion.Text = dictamen.descripcion;
                txt_folio.Content = dictamen.folio;
                txt_fechaHora.Content = dictamen.fechaHora;
                txt_perito.Content = usuario.nombre + " " + usuario.apellidos;

                var query = (from r in db.Reporte
                             join dic in db.Dictamen on r.idReporte equals dic.idReporte
                             join vr in db.VehiculoReporte on r.idReporte equals vr.idReporte
                             join v in db.Vehiculo on vr.placas equals v.placas
                             join c in db.Conductor on v.idConductor equals c.idConductor
                             where vr.idReporte == idReporte
                             select new
                             {
                                 placas = v.placas,
                                 nombre = c.nombre + " " + c.apellidos
                             }).ToList();
                dataGridInvolucrados.ItemsSource = query;

                //ConsultarImagenes
                //IMAGEN 1
                List<byte[]> listaImagenes = new List<byte[]>();

                var queryImagenes = (from i in db.Imagenes
                                     join r in db.Reporte on i.idImagenes equals r.idImagenes
                                     select i.imagen1).FirstOrDefault();

                byte[] bytesImagen1 = queryImagenes;
                listaImagenes.Add(bytesImagen1);

                //IMAGEN 2
                queryImagenes = (from i in db.Imagenes
                                     join r in db.Reporte on i.idImagenes equals r.idImagenes
                                     select i.imagen2).FirstOrDefault();

                byte[] bytesImagen2 = queryImagenes;
                listaImagenes.Add(bytesImagen2);

                //IMAGEN 3
                queryImagenes = (from i in db.Imagenes
                                 join r in db.Reporte on i.idImagenes equals r.idImagenes
                                 select i.imagen3).FirstOrDefault();

                byte[] bytesImagen3 = queryImagenes;
                listaImagenes.Add(bytesImagen3);

                //IMAGEN 4
                queryImagenes = (from i in db.Imagenes
                                 join r in db.Reporte on i.idImagenes equals r.idImagenes
                                 select i.imagen4).FirstOrDefault();

                byte[] bytesImagen4 = queryImagenes;
                listaImagenes.Add(bytesImagen4);

                //IMAGEN 5
                queryImagenes = (from i in db.Imagenes
                                 join r in db.Reporte on i.idImagenes equals r.idImagenes
                                 select i.imagen5).FirstOrDefault();

                byte[] bytesImagen5 = queryImagenes;
                listaImagenes.Add(bytesImagen5);

                //IMAGEN 6
                queryImagenes = (from i in db.Imagenes
                                 join r in db.Reporte on i.idImagenes equals r.idImagenes
                                 select i.imagen6).FirstOrDefault();

                byte[] bytesImagen6 = queryImagenes;
                listaImagenes.Add(bytesImagen6);

                //IMAGEN 7
                queryImagenes = (from i in db.Imagenes
                                 join r in db.Reporte on i.idImagenes equals r.idImagenes
                                 select i.imagen7).FirstOrDefault();

                byte[] bytesImagen7 = queryImagenes;
                listaImagenes.Add(bytesImagen7);

                //IMAGEN 8
                queryImagenes = (from i in db.Imagenes
                                 join r in db.Reporte on i.idImagenes equals r.idImagenes
                                 select i.imagen8).FirstOrDefault();

                byte[] bytesImagen8 = queryImagenes;
                listaImagenes.Add(bytesImagen8);


                for (int i = 0; i < 8; i++)
                {
                    if (listaImagenes[i] != default)
                    {
                        using (MemoryStream ms = new MemoryStream(listaImagenes[i]))
                        {
                            var imageSource = new BitmapImage();
                            imageSource.BeginInit();
                            imageSource.StreamSource = ms;
                            imageSource.CacheOption = BitmapCacheOption.OnLoad;
                            imageSource.EndInit();

                            switch (i + 1)
                            {
                                case 1: 
                                    imagen1.Source = imageSource;
                                    break;
                                case 2:
                                    imagen2.Source = imageSource;
                                    break;
                                case 3:
                                    imagen3.Source = imageSource;
                                    break;
                                case 4:
                                    imagen4.Source = imageSource;
                                    break;
                                case 5:
                                    imagen5.Source = imageSource;
                                    break;
                                case 6:
                                    imagen6.Source = imageSource;
                                    break;
                                case 7:
                                    imagen7.Source = imageSource;
                                    break;
                                case 8:
                                    imagen8.Source = imageSource;
                                    break;
                            }
                            
                        }
                    }
                    
                }
            }
        }

        private void Button_Salir(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
