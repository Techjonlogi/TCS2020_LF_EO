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
using Sistema_DirecciónGeneral.Modelo;

namespace Sistema_DirecciónGeneral
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
                
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
            if (string.IsNullOrEmpty(txt_user.Text) || string.IsNullOrEmpty(txt_pass.Password))
            {
                MessageBox.Show("Usuario y/o password Vacios...", "Error");
                txt_user.Focus();
                txt_pass.Focus();
                return;
            }
            try
            {
                //IQueryable query;
                using (SistemaReportesVehiculosEntities db = new SistemaReportesVehiculosEntities())
                {                   
                    var query = from Usuario in db.Usuarios
                                where Usuario.usuario1 == txt_user.Text && Usuario.contrasenia == txt_pass.Password
                                select Usuario;
                    if(query.Count() > 0)
                    {
                        MessageBox.Show(this, "Bienvenido: " + txt_user.Text, "Información");
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Usuario y/o password incorrecto...", "Error");
                    }
                }
                
            }
            catch
            {
                MessageBox.Show("Error");
                
            }            
        }

    }
}
