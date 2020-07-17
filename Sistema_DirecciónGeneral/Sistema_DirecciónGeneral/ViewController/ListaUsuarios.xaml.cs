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
    /// Lógica de interacción para ListaUsuarios.xaml
    /// </summary>
    public partial class ListaUsuarios : UserControl, IUsuario
    {

   
        public ListaUsuarios()
        {
            InitializeComponent();
            GridUsuario.Children.Clear();
            LlenarTablaUsuarios();
        }

        private void LlenarTablaUsuarios()
        {
            using (SistemaReportesVehiculosEntities db = new SistemaReportesVehiculosEntities())
            {
                var query = (from u in db.Usuario
                             join d in db.Delegacion on u.idDelegación equals d.idDelegacion
                             join crg in db.Cargo on u.idCargo equals crg.idCargo
                             select new
                             {
                                 idUsuario = u.idUsuario,
                                 usuario1 = u.usuario1,
                                 contrasenia = u.contrasenia,
                                 nombre = u.nombre,
                                 apellidos = u.apellidos,
                                 idCargo = u.idCargo,
                                 cargo = crg.tipoCargo,
                                 idDelegacion = u.idDelegación,
                                 delegacion = d.nombre
                             }).ToList();
                dgUsuarios.ItemsSource = query;
            }
        }

        private void btnAgregarUsuario_Click(object sender, RoutedEventArgs e)
        {
            RegistrarUsuario registrarUsuario = new RegistrarUsuario(0, this);
            GridUsuario.Children.Clear();
            GridUsuario.Children.Add(registrarUsuario);
        }

        private void Button_Salir(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Eliminar(object sender, RoutedEventArgs e)
        {
            int idUsuario = (int)((Button)sender).CommandParameter;

            using (SistemaReportesVehiculosEntities db = new SistemaReportesVehiculosEntities())
            {
                var usuario = db.Usuario.Find(idUsuario);

                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("¿Desea eliminarlo?", "Confirmación", System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    db.Usuario.Remove(usuario);
                    db.SaveChanges();

                }

            }
            LlenarTablaUsuarios();
        }

        private void Button_Modificar(object sender, RoutedEventArgs e)
        {
            int idUsuario = (int)((Button)sender).CommandParameter;

            RegistrarUsuario registrarUsuario = new RegistrarUsuario(idUsuario, this);
            GridUsuario.Children.Clear();
            GridUsuario.Children.Add(registrarUsuario);

            LlenarTablaUsuarios();
        }

        
        public void Actualizar(int idUsuario)
        {
            LlenarTablaUsuarios();
        }
    }
}
