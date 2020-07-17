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
    /// Lógica de interacción para DictaminarReporte.xaml
    /// </summary>
    public partial class DictaminarReporte : UserControl
    {
        int idReporte = 0;
        int idUsuario = 0;
        string folio = "";
        string placas =""; 
        IReporte itActualizar;
        public DictaminarReporte(int idUsuario, int idReporte, string folio, IReporte itActualizar)
        {
            InitializeComponent();
            this.idUsuario = idUsuario;
            this.idReporte = idReporte;
            this.folio = folio;
            LlenarResponsable();
            if (this.folio.Length == 12)
            {
                CargarDictamen();

            }
            this.itActualizar = itActualizar;
        }

        private void CargarDictamen()
        {
            using(SistemaReportesVehiculosEntities db = new SistemaReportesVehiculosEntities())
            {
                Dictamen dictamen = db.Dictamen.Find(folio);
                cb_responsable.Text = dictamen.responsable;
                txt_descripcion.Text = dictamen.descripcion;
            }
        }

        private void LlenarResponsable()
        {
            SistemaReportesVehiculosEntities db = new SistemaReportesVehiculosEntities();
            var list = (from r in db.Reporte
                        join dic in db.Dictamen on r.idReporte equals dic.idReporte
                        join vr in db.VehiculoReporte on r.idReporte equals vr.idReporte
                        join v in db.Vehiculo on vr.placas equals v.placas
                        join c in db.Conductor on v.idConductor equals c.idConductor
                        where vr.idReporte == idReporte
                        select new
                        {
                            idReporte = vr.idReporte,
                            placas = vr.placas,
                            auto = v.placas
                        }).ToList();

            if (list.Count() > 0)
            {
                cb_responsable.ItemsSource = list;
                cb_responsable.DisplayMemberPath = "auto";
                cb_responsable.SelectedValue = "placas";
            }
        }

        private void Button_Salir(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btn_dictaminar_Click(object sender, RoutedEventArgs e)
        {
            string descripcion = txt_descripcion.Text;
            string hora = DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString();
            string fecha = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString();
            placas = cb_responsable.Text;

            int idReportAux = idReporte;

            if (string.IsNullOrEmpty(descripcion))
            {
                MessageBox.Show("Campos Vacios...", "Error");
                return;
            }
            try
            {
               
                    using (SistemaReportesVehiculosEntities db = new SistemaReportesVehiculosEntities())
                    {
                    Dictamen dictamenNew = db.Dictamen.Find(folio);
                    dictamenNew.folio = fecha + hora;
                    dictamenNew.descripcion = descripcion;
                    dictamenNew.responsable = placas;
                    dictamenNew.fechaHora = DateTime.Now;
                    dictamenNew.idUsuario = idUsuario;
                    dictamenNew.idReporte = idReportAux;
                    db.Entry(dictamenNew).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    MessageBox.Show("dictamen actualizado con éxito");
                    idReportAux = dictamenNew.idReporte;
                    }           
                this.Visibility = Visibility.Collapsed;
                this.itActualizar.Actualizar(idReportAux);
            }


            catch
            {
                MessageBox.Show("Error");

            }
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

    }
}
