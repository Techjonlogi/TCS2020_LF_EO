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

namespace Sistema_DelegacionMunicipal.ChatMsj
{
    /// <summary>
    /// Lógica de interacción para MensajeEnviado.xaml
    /// </summary>
    public partial class MensajeEnviado : UserControl
    {
        public MensajeEnviado(int posicionMensaje, string mensaje)
        {
            InitializeComponent();
            txtMensaje.Text = mensaje;
            this.Margin = new Thickness(75, posicionMensaje, 35, 0);

            if (DateTime.Now.Hour > 12) 
                 txtHora.Text = "" + (DateTime.Now.Hour - 12) + ":" + DateTime.Now.Minute + " pm";
            else
                txtHora.Text = "" + (DateTime.Now.Hour) + ":" + DateTime.Now.Minute + " am";

        }
    }
}
