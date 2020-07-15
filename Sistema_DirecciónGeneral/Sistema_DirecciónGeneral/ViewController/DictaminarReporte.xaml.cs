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
        IReporte itActualizar;
        public DictaminarReporte(int idReporte, IReporte itActualizar)
        {
            InitializeComponent();
            this.idReporte = idReporte;
            if (this.idReporte > 0)
            {
                LlenarResponsable();

            }
            this.itActualizar = itActualizar;
        }

        private void LlenarResponsable()
        {
            SistemaReportesVehiculosEntities db = new SistemaReportesVehiculosEntities();
            var list = (from r in db.Reporte
                        join vr in db.VehiculoReporte on r.idReporte equals vr.idReporte
                        join v in db.Vehiculo on vr.placas equals v.placas
                        select new
                        {
                            idReporte = vr.idReporte,
                            placas = vr.placas,
                            auto = v.marca + " " + v.modelo + " " + v.placas
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
            string responsable = cb_responsable.Text;
            string hora = DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString();
            string fecha = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString();
            //int idUsuario = 

            int idReportAux = idReporte;

            if (string.IsNullOrEmpty(descripcion) || string.IsNullOrEmpty(responsable))
            {
                MessageBox.Show("Campos Vacios...", "Error");
                return;
            }
            try
            {
                if (idReporte == 0)
                {
                    using (SistemaReportesVehiculosEntities db = new SistemaReportesVehiculosEntities())
                    {
                        Dictamen dictamenNew = new Dictamen
                        {
                            folio = fecha + hora,
                            descripcion = descripcion,
                            responsable = responsable,
                            fechaHora = DateTime.Now,
                            idUsuario = 5, //Traer el idUsuario desde el login
                            idReporte = idReportAux
                        };
                        db.Dictamen.Add(dictamenNew);
                        db.SaveChanges();
                        MessageBox.Show("Usuario registrado con éxito");
                        idReportAux = dictamenNew.idReporte;
                    }
                }
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
