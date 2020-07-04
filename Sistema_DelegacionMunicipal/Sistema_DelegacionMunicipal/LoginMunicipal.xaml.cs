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
using System.Windows.Shapes;

namespace Sistema_DelegacionMunicipal
{
    /// <summary>
    /// Lógica de interacción para LoginMunicipal.xaml
    /// </summary>
    public partial class LoginMunicipal : Window
    {
        public LoginMunicipal()
        {
            InitializeComponent();
        }

        private void Button_Cerrar(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_IniciarSesion(object sender, RoutedEventArgs e)
        {
            
            if (string.IsNullOrEmpty(txt_user.Text) || string.IsNullOrEmpty(txt_pass.Password))
            {
                MessageBox.Show("Usuario y/o password Vacios...", "Error");
                return;
            }
            try
            {
                //IQueryable query;
                using (SistemaReportesVehiculosEntities db = new SistemaReportesVehiculosEntities())
                {
                    var query = from Usuario in db.Usuario
                                where Usuario.usuario1 == txt_user.Text && Usuario.contrasenia == txt_pass.Password
                                select Usuario.idUsuario;
                    if (query.Count() > 0)
                    {
                        int idUser = db.Usuario.Where(x => x.usuario1 == txt_user.Text).Select(x => x.idUsuario).FirstOrDefault();

                        MessageBox.Show(this, "Bienvenido: " + txt_user.Text, "Información");
                        MainWindow mainWindow = new MainWindow(idUser);
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
            /*
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            this.Close();
            */
        }
    }
}
