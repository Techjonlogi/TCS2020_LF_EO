using Sistema_DirecciónGeneral.Interfaces;
using Sistema_DirecciónGeneral.Modelo;
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
    /// Lógica de interacción para DetallesReporte.xaml
    /// </summary>
    public partial class DetallesReporte : UserControl
    {
        IReporte itActualizar;
        int idReporte = 0;
        string folio = "";
        string placasRespon = "";
        
        public DetallesReporte(int idReporte, string folio, string placasRespon, IReporte itActualizar)
        {
            InitializeComponent();
            this.idReporte = idReporte;
            this.folio = folio;
            this.placasRespon = placasRespon;
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
                Conductor conductor = db.Conductor.Find(vehiculo.idConductor);
                txt_delegacion.Content = delegacion.nombre;
                txt_responsable.Content = conductor.nombre + " " + conductor.apellidos;
                txt_vehiculo.Content = vehiculo.marca + " " + vehiculo.modelo + " " + vehiculo.placas;
                if(folio.Length == 0)
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


                var query = (from r in db.Reporte
                             join dic in db.Dictamen on r.idReporte equals dic.idReporte
                             join vr in db.VehiculoReporte on r.idReporte equals vr.idReporte
                             join v in db.Vehiculo on vr.placas equals v.placas
                             join c in db.Conductor on v.idConductor equals c.idConductor
                             select new
                             {
                                 idReporte = r.idReporte,
                                 direccion = r.direccion,
                                 numCarrosInvolucrados = r.numCarrosInvolucrados,
                                 idDelegación = r.idDelegación,
                                 idImagenes = r.idImagenes,
                                 placas = v.placas,
                                 folio = dic.folio,
                                 nombre = c.nombre + " " + c.apellidos
                             }).ToList();
                dataGridInvolucrados.ItemsSource = query;
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
