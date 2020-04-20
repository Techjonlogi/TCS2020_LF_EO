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
using Sistema_DirecciónGeneral.ViewController;

namespace Sistema_DirecciónGeneral
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int botonSeleccionado = 5;

        ChatGrupal chat = new ChatGrupal();
        
        Inicio inicio = new Inicio();

        public MainWindow()
        {
            InitializeComponent();
            GridPrincipal.Children.Add(inicio);
            btnInicio.Background = Brushes.White;
        }


        private void BtnRegistrarUsuario_Click(object sender, RoutedEventArgs e)
        {
            if (botonSeleccionado != 1)
            {
                cambiarBoton(1);
                GridPrincipal.Children.Clear();
                GridPrincipal.Children.Add(new RegistrarUsuario());
            }
        }

        private void BtnRegistrarDelegacion_Click(object sender, RoutedEventArgs e)
        {
            if (botonSeleccionado != 2)
            {
                cambiarBoton(2);
                GridPrincipal.Children.Clear();
                GridPrincipal.Children.Add(new RegistrarDelegacion());
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

        private void BtnDictaminar_Click(object sender, RoutedEventArgs e)
        {
            if (botonSeleccionado != 4)
            {
                cambiarBoton(3);
                GridPrincipal.Children.Clear();
                GridPrincipal.Children.Add(new DictaminarReporte());
            }
        }

        private void btnChat_Click(object sender, RoutedEventArgs e)
        {
            if (botonSeleccionado != 5)
            {
                cambiarBoton(5);
                GridPrincipal.Children.Clear();
                GridPrincipal.Children.Add(chat);
            }
        }

        private void BtnInicio_Click(object sender, RoutedEventArgs e)
        {
            if (botonSeleccionado != 6)
            {
                cambiarBoton(6);
                GridPrincipal.Children.Clear();
                GridPrincipal.Children.Add(inicio);
            }
        }


        private void cambiarBoton(int seleccionado)
        {
            btnRegistrarUsuario.Background = Brushes.Transparent;
            btnRegistrarDelegacion.Background = Brushes.Transparent;
            btnVisualizarReportes.Background = Brushes.Transparent;
            btnDictaminar.Background = Brushes.Transparent;
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
                    btnDictaminar.Background = Brushes.White;
                    break;

                case (5):
                    botonSeleccionado = 5;
                    btnChat.Background = Brushes.White;
                    break;
                case (6):
                    botonSeleccionado = 6;
                    btnInicio.Background = Brushes.White;
                    break;
            }

        }


    }
}