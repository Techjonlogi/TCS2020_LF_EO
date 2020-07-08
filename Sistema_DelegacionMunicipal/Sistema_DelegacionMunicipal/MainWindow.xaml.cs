using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Sistema_DelegacionMunicipal.Modelo;
using Sistema_DelegacionMunicipal.ViewController;

namespace Sistema_DelegacionMunicipal
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int botonSeleccionado = 5;
        int idUser = 1;

        Chat chat; 
        LevantarReporte levantarReporte = new LevantarReporte();
        Inicio inicio = new Inicio();

        public MainWindow(int idUser)
        {
            InitializeComponent();
            GridPrincipal.Children.Add(inicio);
            btnInicio.Background = Brushes.White;


            SistemaReportesVehiculosEntities db = new SistemaReportesVehiculosEntities();
            string usuarioEmisor = db.Usuario.Where(x => x.idUsuario == idUser).Select(x => x.usuario1).FirstOrDefault().ToString();

            string delegacionEmisor = (from u in db.Usuario.Where(x => x.idUsuario == idUser)
                                       from d in db.Delegacion.Where(x => x.idDelegacion == u.idDelegación)
                                       select d.nombre).FirstOrDefault().ToString();


            this.idUser = idUser;
            chat = new Chat(idUser, usuarioEmisor, delegacionEmisor);
        }






        private void cuadroUsuario_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }


        private void BtnConductores_Click(object sender, RoutedEventArgs e)
        {
            if (botonSeleccionado != 1)
            {
                cambiarBoton(1);
                GridPrincipal.Children.Clear();
                GridPrincipal.Children.Add(new ListaConductor());
            }
        }

        private void BtnLevantarReporte_Click(object sender, RoutedEventArgs e)
        {
            if (botonSeleccionado != 2)
            {
                cambiarBoton(2);
                GridPrincipal.Children.Clear();
                GridPrincipal.Children.Add(levantarReporte);
            }
        }

        private void BtnHistorialReportes_Click(object sender, RoutedEventArgs e)
        {
            if (botonSeleccionado != 3)
            {
                cambiarBoton(3);
                GridPrincipal.Children.Clear();
                GridPrincipal.Children.Add(new HistorialReportes());
            }
        }

        private void btnChat_Click(object sender, RoutedEventArgs e)
        {
            if (botonSeleccionado != 4)
            {
                cambiarBoton(4);
                GridPrincipal.Children.Clear();
                GridPrincipal.Children.Add(chat);
            }
        }

        private void BtnInicio_Click(object sender, RoutedEventArgs e)
        {
            if (botonSeleccionado != 5)
            {
                cambiarBoton(5);
                GridPrincipal.Children.Clear();
                GridPrincipal.Children.Add(inicio);
            }
        }


        private void cambiarBoton(int seleccionado)
        {

            btnConductores.Background = Brushes.Transparent;
            btnLevantarReporte.Background = Brushes.Transparent;
            btnHistorialReportes.Background = Brushes.Transparent;
            btnChat.Background = Brushes.Transparent;
            btnInicio.Background = Brushes.Transparent;

            switch (seleccionado)
            {
                case (0):

                    break;

                case (1):
                    botonSeleccionado = 1;
                    btnConductores.Background = Brushes.White;
                    break;

                case (2):
                    botonSeleccionado = 2;
                    btnLevantarReporte.Background = Brushes.White;
                    break;

                case (3):
                    botonSeleccionado = 3;
                    btnHistorialReportes.Background = Brushes.White;
                    break;

                case (4):
                    botonSeleccionado = 4;
                    btnChat.Background = Brushes.White;
                    break;

                case (5):
                    botonSeleccionado = 5;
                    btnInicio.Background = Brushes.White;
                    break;
            }

        }

        private void BtnInicio_MouseEnter(object sender, MouseEventArgs e)
        {
            btnInicio.Background = Brushes.White;
        }

        private void BtnInicio_MouseLeave(object sender, MouseEventArgs e)
        {
            if (botonSeleccionado != 5)
                btnInicio.Background = Brushes.Transparent;
        }

        private void btnConductores_MouseEnter(object sender, MouseEventArgs e)
        {
            btnConductores.Background = Brushes.White;
        }

        private void btnConductores_MouseLeave(object sender, MouseEventArgs e)
        {
            if (botonSeleccionado != 1)
                btnConductores.Background = Brushes.Transparent;
        }

        private void btnLevantarReporte_MouseEnter(object sender, MouseEventArgs e)
        {
            btnLevantarReporte.Background = Brushes.White;
        }

        private void btnLevantarReporte_MouseLeave(object sender, MouseEventArgs e)
        {
            if (botonSeleccionado != 2)
                btnLevantarReporte.Background = Brushes.Transparent;
        }

        private void btnHistorialReportes_MouseEnter(object sender, MouseEventArgs e)
        {
            btnHistorialReportes.Background = Brushes.White;
        }

        private void btnHistorialReportes_MouseLeave(object sender, MouseEventArgs e)
        {
            if (botonSeleccionado != 3)
                btnHistorialReportes.Background = Brushes.Transparent;
        }

        private void btnChat_MouseEnter(object sender, MouseEventArgs e)
        {
            btnChat.Background = Brushes.White;
        }

        private void btnChat_MouseLeave(object sender, MouseEventArgs e)
        {
            if (botonSeleccionado != 4)
                btnChat.Background = Brushes.Transparent;
        }

        
    }
}
