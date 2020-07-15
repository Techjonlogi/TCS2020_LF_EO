using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
using Sistema_DirecciónGeneral.Modelo;
using Sistema_DirecciónGeneral.ViewController;

namespace Sistema_DirecciónGeneral
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int botonSeleccionado = 5;

        ChatGrupal chatGrupal;
        Inicio inicio = new Inicio();
        private int idUser;

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

            int idDelegacionEmisor = (from u in db.Usuario.Where(x => x.idUsuario == idUser)
                                      from d in db.Delegacion.Where(x => x.idDelegacion == u.idDelegación)
                                      select d.idDelegacion).FirstOrDefault();

            this.idUser = idUser;

            Socket socketCliente = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            chatGrupal = new ChatGrupal(idUser, usuarioEmisor, delegacionEmisor, socketCliente);

            inicio.MensajeBienvenida(usuarioEmisor);
        }


        private void BtnRegistrarUsuario_Click(object sender, RoutedEventArgs e)
        {
            if (botonSeleccionado != 1)
            {
                cambiarBoton(1);
                GridPrincipal.Children.Clear();
                GridPrincipal.Children.Add(new ListaUsuarios());
            }
        }

        private void BtnRegistrarDelegacion_Click(object sender, RoutedEventArgs e)
        {
            if (botonSeleccionado != 2)
            {
                cambiarBoton(2);
                GridPrincipal.Children.Clear();
                GridPrincipal.Children.Add(new ListaDelegaciones());
            }
        }

        private void BtnVisualizarReportes_Click(object sender, RoutedEventArgs e)
        {
            if (botonSeleccionado != 3)
            {
                cambiarBoton(3);
                GridPrincipal.Children.Clear();
                GridPrincipal.Children.Add(new VisualizarReportes());
            }
        }


        private void btnChat_Click(object sender, RoutedEventArgs e)
        {
            if (botonSeleccionado != 4)
            {
                cambiarBoton(4);
                GridPrincipal.Children.Clear();
                GridPrincipal.Children.Add(chatGrupal);
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
            btnRegistrarUsuario.Background = Brushes.Transparent;
            btnRegistrarDelegacion.Background = Brushes.Transparent;
            btnVisualizarReportes.Background = Brushes.Transparent;
            btnChat.Background = Brushes.Transparent;
            btnInicio.Background = Brushes.Transparent;

            switch (seleccionado)
            {
                case (0):

                    break;

                case (1):
                    botonSeleccionado = 1;
                    btnRegistrarUsuario.Background = Brushes.White;
                    break;

                case (2):
                    botonSeleccionado = 2;
                    btnRegistrarDelegacion.Background = Brushes.White;
                    break;

                case (3):
                    botonSeleccionado = 3;
                    btnVisualizarReportes.Background = Brushes.White;
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

        private void CuadroUsuario_MouseDown(object sender, MouseButtonEventArgs e)
        {

            DragMove();
        }
    }
}