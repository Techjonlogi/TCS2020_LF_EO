using Sistema_DelegacionMunicipal.ChatMsj;
using Sistema_DelegacionMunicipal.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Sistema_DelegacionMunicipal.ViewController
{
    /// <summary>
    /// Lógica de interacción para Chat.xaml
    /// </summary>
    public partial class Chat : UserControl
    {
        int posicionMensaje = 0;


        public Chat()
        {
            InitializeComponent();
            ManejoBWorker();
        }


        public void ManejoBWorker()
        {
            BackgroundWorker bw = new BackgroundWorker();
            SocketServer ss = new SocketServer();
            bw.WorkerReportsProgress = true;
            bw.DoWork +=  ss.Conectar;
            bw.RunWorkerCompleted += metodoCompleted;
            bw.RunWorkerAsync();

        }

        public void metodoDoWork(object sender, DoWorkEventArgs e)
        {
            //Implementación en segundo plano

        }

        public void metodoCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Hilo principal reporta finalización de tarea
            string resultado = (string)e.Result;
            recibirMensaje(resultado);

            txtBoxMensaje.Text = "";
            txtBoxMensaje.Focus();

            ManejoBWorker();
        }







    private void txtBoxMensaje_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            enviarMensaje();
        }
    }
    

    private void BtnSalir_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }



    private void btnEnviar_Click(object sender, RoutedEventArgs e)
    {
            enviarMensaje();
    }


        private void btnEnviarImagen_Click(object sender, RoutedEventArgs e)
    {

    }

    public void recibirMensaje(string mensajeRecibido)
    {

        scrollChat.ScrollToVerticalOffset(GridBaseChatRecibido.Height - 1);

        GridChatRecibido.Children.Add(new MensajeChat(posicionMensaje, mensajeRecibido));

        txtMensajeRecibido.Text = mensajeRecibido;

        int separacion = 0;
        if (txtMensajeRecibido.LineCount == 1)
            separacion = 65;
        if (txtMensajeRecibido.LineCount == 2)
            separacion = 80;
        if (txtMensajeRecibido.LineCount == 3)
            separacion = 95;
        if (txtMensajeRecibido.LineCount > 3)
            separacion = 110;

        posicionMensaje += separacion;

        if (posicionMensaje > 554)
        {
            GridBaseChatRecibido.Height += separacion;
            GridChatRecibido.Height += separacion;
        }

    }

    public void enviarMensaje()
    {
        string texto = txtBoxMensaje.Text.Trim();

        if (texto != "")
        {
            SocketCliente sc = new SocketCliente();
            sc.Conectar(texto);

            scrollChat.ScrollToVerticalOffset(GridBaseChatRecibido.Height - 1);

            GridChatRecibido.Children.Add(new MensajeEnviado(posicionMensaje, texto));

            int separacion = 0;
            if (txtBoxMensaje.LineCount == 1)
                separacion = 65;
            if (txtBoxMensaje.LineCount == 2)
                separacion = 80;
            if (txtBoxMensaje.LineCount == 3)
                separacion = 95;
            if (txtBoxMensaje.LineCount > 3)
                separacion = 110;

            posicionMensaje += separacion;

            if (posicionMensaje > 554)
            {
                GridBaseChatRecibido.Height += separacion;
                GridChatRecibido.Height += separacion;
            }
        }


    }

        private void txtBoxMensaje_GotFocus(object sender, RoutedEventArgs e)
        {
            lbMensaje.Content = "";
        }

        private void txtBoxMensaje_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtBoxMensaje.Text == "")
                lbMensaje.Content = "Escribe un mensaje";
        }
    }
           
}
