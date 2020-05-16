using Sistema_DirecciónGeneral.DAOs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
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
    /// Lógica de interacción para RegistrarUsuario.xaml
    /// </summary>
    public partial class RegistrarUsuario : UserControl
    {
        SqlConnection conn = null;
        public RegistrarUsuario()
        {
            InitializeComponent();
           
        }
        private void Button_Salir(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

private void Btn_RegistrarUsuario(object sender, RoutedEventArgs e)
        {

            UsuarioDAO usuarioDAO = new UsuarioDAO();
            usuarioDAO.AddUsuario(txt_user.Text, txt_contrasenia.Text, txt_nombre.Text, txt_apellidos.Text, cb_cargo.Text, cb_delegacion.Text);
        }

        private void cb_delegacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cb_cargo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
