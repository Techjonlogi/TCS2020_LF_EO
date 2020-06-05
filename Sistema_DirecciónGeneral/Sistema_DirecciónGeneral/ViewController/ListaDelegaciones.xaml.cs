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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sistema_DirecciónGeneral.ViewController
{
    /// <summary>
    /// Lógica de interacción para ListaDelegaciones.xaml
    /// </summary>
    public partial class ListaDelegaciones : UserControl
    {
        public ListaDelegaciones()
        {
            InitializeComponent();
            GridDelegacion.Children.Clear();
            LlenarTablaDelegacion();
        }
        private void LlenarTablaDelegacion()
        {
            using (SistemaReportesVehiculosEntities db = new SistemaReportesVehiculosEntities())
            {
                var query = (from d in db.Delegacion
                             join m in db.Municipio on d.idMunicipio equals m.idMunicipio
                             select new
                             {
                                 nombre = d.nombre,
                                 municipio = m.municipio1,
                                 telefono = d.telefono,
                                 correo = d.correo                                 
                             }).ToList();
                dgDelegacion.ItemsSource = query;
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Salir(object sender, RoutedEventArgs e)
        {

        }

        private void btnEditarDelegacion_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRegistrarDelegacion_Click(object sender, RoutedEventArgs e)
        {
            RegistrarDelegacion registrarDelegacion = new RegistrarDelegacion();
            GridDelegacion.Children.Clear();
            GridDelegacion.Children.Add(registrarDelegacion);
        }
    }
}
