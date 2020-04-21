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

namespace Sistema_DirecciónGeneral.ViewController
{
    /// <summary>
    /// Lógica de interacción para DictaminarReporte.xaml
    /// </summary>
    public partial class DictaminarReporte : UserControl
    {
        public DictaminarReporte()
        {
            InitializeComponent();
        }

        private void Button_Salir(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
