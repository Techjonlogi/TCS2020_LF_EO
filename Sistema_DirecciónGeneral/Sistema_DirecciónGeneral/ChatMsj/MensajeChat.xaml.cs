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

namespace Sistema_DirecciónGeneral.ChatMsj
{
    /// <summary>
    /// Lógica de interacción para MensajeChat.xaml
    /// </summary>
    public partial class MensajeChat : UserControl
    {
        public MensajeChat(int posicionMensaje, string mensaje, string usuarioEmisor)
        {
            InitializeComponent();

            txtMensaje.Text = mensaje;

            if (usuarioEmisor.Length > 4)
            {
                txtEmisor.Text = usuarioEmisor.Remove(4, usuarioEmisor.Length - 4);
            }
            else
            {
                txtEmisor.Text = usuarioEmisor;
            }

            this.Margin = new Thickness(0, posicionMensaje, 71, 0);

            string minutos = "";

            if (DateTime.Now.Minute < 10)
                minutos = "0" + DateTime.Now.Minute;
            else
                minutos = "" + DateTime.Now.Minute;

            if (DateTime.Now.Hour > 12)
                txtHora.Text = "" + (DateTime.Now.Hour - 12) + ":" + minutos + " pm";
            else
                txtHora.Text = "" + (DateTime.Now.Hour) + ":" + minutos + " am";

        }

    }
}

