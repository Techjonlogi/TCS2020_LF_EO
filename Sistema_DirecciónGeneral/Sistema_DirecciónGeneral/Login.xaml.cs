using Sistema_DirecciónGeneral.Clases;
using Sistema_DirecciónGeneral.DAOs;
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

namespace Sistema_DirecciónGeneral
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private string user;
        private string contrasenia;
        
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Salir(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_IniciarSesion(object sender, RoutedEventArgs e)
        {
            

            if (validarCampos())
            {
                user = txt_user.Text;
                contrasenia = txt_pass.Password;
                               
                Usuario userGeneral = UsuarioDAO.GetLogin(user, contrasenia);
                //Login
                if (userGeneral != null && userGeneral.Idusuario > 0)
                {
                    MessageBox.Show(this, "Bienvenido: " + userGeneral.Nombreuser, "Información");
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show(this, "Sin acceso", "Error");
                    txt_user.Text = "";
                    txt_pass.Password = "";
                    txt_user.Focus();
                }

            }
            else
            {
                MessageBox.Show("Usuario y/o password Vacios...", "Error");
            }
        }

        private bool validarCampos()
        {
            if (txt_user.Text == null || txt_user.Text.Length == 0)
            {
                return false;
            }
            if (txt_pass.Password == null || txt_pass.Password.Length == 0)
            {
                return false;
            }
            return true;
        }

    }
}
