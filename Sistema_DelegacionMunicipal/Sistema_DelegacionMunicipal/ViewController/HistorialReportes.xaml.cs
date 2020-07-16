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
    /// Lógica de interacción para HistorialReportes.xaml
    /// </summary>
    public partial class HistorialReportes : UserControl, IReporte
    {

        int idUsuarioSelec = 0;
        public HistorialReportes(int idUsuario)
        {
            this.idUsuarioSelec = idUsuario;
            InitializeComponent();
            gridHistorial.Children.Clear();
            LlenarTablaHistorial();
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }



        private void BtnVerDictamen_Click(object sender, RoutedEventArgs e)
        {
            DetalleDictamen dictamen = new DetalleDictamen();
            gridHistorial.Children.Clear();
            gridHistorial.Children.Add(dictamen);
        }

        private void LlenarTablaHistorial()
        {
            using (SistemaReportesVehiculosEntities db = new SistemaReportesVehiculosEntities())
            {
                var query = (from r in db.Reporte
                             join d in db.Delegacion on r.idDelegación equals d.idDelegacion
                             where r.idDelegación == d.idDelegacion
                             select new
                             {
                                 idReporte = r.idReporte,
                                 direccion = r.direccion,
                                 numCarrosInvolucrados = r.numCarrosInvolucrados,
                                 fechaHora = r.fechaHora
                             }).ToList();
                
                dataGridInvolucrados.ItemsSource = query;
            }
        }


        public void Actualizar(int idReporte)
        {
            LlenarTablaHistorial();
        }
    }
}