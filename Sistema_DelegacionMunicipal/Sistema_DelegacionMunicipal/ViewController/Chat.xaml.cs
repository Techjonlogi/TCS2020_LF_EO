using Sistema_DelegacionMunicipal.ChatMsj;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sistema_DelegacionMunicipal.ViewController
{
    /// <summary>
    /// Lógica de interacción para Chat.xaml
    /// </summary>
    public partial class Chat : UserControl
    {
        int posicionMensaje = 0;
        bool gridAmpliado = false;
        string mensaje = "";

        public Chat()
        {
            InitializeComponent();

            Conectar();
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
            //Crear user control del mensaje

            GridChatRecibido.Children.Add(new MensajeChat(posicionMensaje, mensajeRecibido));

            txtMensajeRecibido.Text = mensajeRecibido;

            //Actualizar valores de separación de mensaje

            int separacion = 0;

            separacion += txtMensajeRecibido.LineCount * 16 + 45;

            int posicionBuffer = posicionMensaje;

            posicionMensaje += separacion;

            //Ajustar tamaño de grid

            if (posicionMensaje > 554)
            {

                if (!gridAmpliado)
                {
                    GridBaseChatRecibido.Height += (separacion - (554 - posicionBuffer));
                    GridChatRecibido.Height += (separacion - (554 - posicionBuffer));
                }
                else
                {
                    GridBaseChatRecibido.Height += separacion;
                    GridChatRecibido.Height += separacion;
                }

                gridAmpliado = true;


            }

            scrollChat.ScrollToVerticalOffset(GridBaseChatRecibido.Height - 1);

        }

        public void enviarMensaje()
        {
            string texto = txtBoxMensaje.Text.Trim();

            if (texto != "")
            {

                envioMsj(texto);
                txtMensajeRecibido.Text = texto;

                //Crear user control del mensaje

                GridChatRecibido.Children.Add(new MensajeEnviado(posicionMensaje, texto));


                //Actualizar valores de separación de mensaje

                int separacion = 0;

                separacion += txtMensajeRecibido.LineCount * 16 + 45;

                int posicionBuffer = posicionMensaje;

                posicionMensaje += separacion;

                //Ajustar tamaño de grid

                if (posicionMensaje > 554)
                {

                    if (!gridAmpliado)
                    {
                        GridBaseChatRecibido.Height += (separacion - (554 - posicionBuffer));
                        GridChatRecibido.Height += (separacion - (554 - posicionBuffer));
                    }
                    else
                    {
                        GridBaseChatRecibido.Height += separacion;
                        GridChatRecibido.Height += separacion;
                    }

                    gridAmpliado = true;


                }

                scrollChat.ScrollToVerticalOffset(GridBaseChatRecibido.Height - 1);
            }


            txtBoxMensaje.Text = "";
            txtBoxMensaje.Focus();
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





        // MANEJO DE MENSAJES

        private Socket socketCliente = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


        private void Conectar()
        {
            LoopConnect();

            socketCliente.BeginReceive(receivedBuf, 0, receivedBuf.Length, SocketFlags.None, new AsyncCallback(ReceiveData), socketCliente);
        }





        byte[] receivedBuf = new byte[1024];

        //Recibir información del servidor
        private void ReceiveData(IAsyncResult ar)
        {
            Socket socket = (Socket)ar.AsyncState;
            int received = socket.EndReceive(ar);
            byte[] dataBuf = new byte[received];
            Array.Copy(receivedBuf, dataBuf, received);
            mensaje = (Encoding.Default.GetString(dataBuf));

            this.Dispatcher.Invoke(() =>
            {
                recibirMensaje(mensaje);
            });

            socketCliente.BeginReceive(receivedBuf, 0, receivedBuf.Length, SocketFlags.None, new AsyncCallback(ReceiveData), socketCliente);


        }



        private void LoopConnect()
        {
            while (!socketCliente.Connected)
            {
                try
                {
                    socketCliente.Connect(IPAddress.Loopback, 100);
                }
                catch (SocketException)
                {

                }
            }
        }

        private void envioMsj(string msj)
        {
            if (socketCliente.Connected)
            {

                byte[] buffer = Encoding.Default.GetBytes(msj);
                socketCliente.Send(buffer);
            }

        }


    }
}

