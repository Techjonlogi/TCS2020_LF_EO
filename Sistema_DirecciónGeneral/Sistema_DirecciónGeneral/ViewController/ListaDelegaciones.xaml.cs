using MaterialDesignThemes.Wpf;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sistema_DirecciónGeneral.ViewController
{
    /// <summary>
    /// Lógica de interacción para ListaDelegaciones.xaml
    /// </summary>
    public partial class ListaDelegaciones : UserControl, IDelegacion
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
                                 idDelegacion = d.idDelegacion,
                                 nombre = d.nombre,
                                 calle = d.calle,
                                 numero = d.numero,
                                 colonia = d.colonia,
                                 codigoPostal = d.codigoPostal,
                                 idMunicipio = d.idMunicipio,
                                 municipio = m.municipio1,
                                 telefono = d.telefono,
                                 correo = d.correo                                 
                             }).ToList();
                dgDelegacion.ItemsSource = query;
            }
        }     

        private void Button_Salir(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }    

        private void btnRegistrarDelegacion_Click(object sender, RoutedEventArgs e)
        {
            RegistrarDelegacion registrarDelegacion = new RegistrarDelegacion(0, this);
            GridDelegacion.Children.Clear();
            GridDelegacion.Children.Add(registrarDelegacion);
        }

        private void Button_Eliminar(object sender, RoutedEventArgs e)
        {
            int idDelegacion = (int)((Button)sender).CommandParameter;

            using(SistemaReportesVehiculosEntities db = new SistemaReportesVehiculosEntities())
            {
                var delegacion = db.Delegacion.Find(idDelegacion);

                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("¿Desea eliminarlo?", "Confirmación", System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                db.Delegacion.Remove(delegacion);
                db.SaveChanges();

                }

            }
            LlenarTablaDelegacion();
        }

        private void Button_Modificar(object sender, RoutedEventArgs e)
        {
            int idDelegacion = (int)((Button)sender).CommandParameter;

            RegistrarDelegacion registrarDelegacion = new RegistrarDelegacion(idDelegacion, this);
            GridDelegacion.Children.Clear();
            GridDelegacion.Children.Add(registrarDelegacion);

            LlenarTablaDelegacion();
        }

        public void Actualizar(int idDelegacion)
        {
            LlenarTablaDelegacion();
        }
    }
}
