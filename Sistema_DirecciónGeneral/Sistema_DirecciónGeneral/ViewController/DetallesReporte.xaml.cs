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
        
        public DetallesReporte(int idReporte, IReporte itActualizar)
        {
            InitializeComponent();
            this.idReporte = idReporte;
            this.itActualizar = itActualizar;
            CargarDetallesReportes();
        }

        private void CargarDetallesReportes()
        {
            using (SistemaReportesVehiculosEntities db = new SistemaReportesVehiculosEntities())
            {
                Reporte reporte = db.Reporte.Find(idReporte);
                txt_direccion.Text = reporte.direccion;
                
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
