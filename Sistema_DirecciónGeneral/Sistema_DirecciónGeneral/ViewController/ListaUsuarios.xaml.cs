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
    public partial class ListaUsuarios : UserControl
    {

        int usuarioSeleccionado = 0;
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
                             select new
                             {
                                 idUsuario = u.idUsuario,
                                 usuario1 = u.usuario1,
                                 nombre = u.nombre,
                                 cargo = u.cargo,
                                 delegacion = d.nombre
                             }).ToList();
                dgUsuarios.ItemsSource = query;
            }
        }

        private void btnAgregarUsuario_Click(object sender, RoutedEventArgs e)
        {
            RegistrarUsuario registrarUsuario = new RegistrarUsuario();
            GridUsuario.Children.Clear();
            GridUsuario.Children.Add(registrarUsuario);
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEditarUsuario_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Salir(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void dgUsuarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            usuarioSeleccionado = dgUsuarios.SelectedIndex + 1;
        }
    }
}
