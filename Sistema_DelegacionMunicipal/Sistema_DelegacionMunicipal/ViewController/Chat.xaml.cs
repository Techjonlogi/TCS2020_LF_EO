using Sistema_DelegacionMunicipal.ChatMsj;
using Sistema_DelegacionMunicipal.Classes;
using Sistema_DelegacionMunicipal.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;

namespace Sistema_DelegacionMunicipal.ViewController
{
    /// <summary>
    /// Lógica de interacción para Chat.xaml
    /// </summary>
    public partial class Chat : UserControl
    {
        EnvioMensajeChat envioMensajeChat = new EnvioMensajeChat();
        SistemaReportesVehiculosEntities db = new SistemaReportesVehiculosEntities();

        int posicionMensaje = 0;
        bool gridAmpliado = false;
        string mensaje = "";
        string usuarioEmisor = "";
        string delegacionEmisor = "";

        List<string> listaConectados = new List<string>();

        public Chat(int idUser, string usuarioEmisor, string delegacionEmisor)
        {
            InitializeComponent();
            this.usuarioEmisor = usuarioEmisor;
            this.delegacionEmisor = delegacionEmisor;

            intentarConexion();
        }

        


        //Verificar que el servidor esté activo
        public void intentarConexion()
        {
            ocultarErrorConexion();
            
            try
            {
                Conectar();
            }
            catch (Exception)
            {
                mostrarErrorConexion();
            }
        }


        private void btnReintentarConexión_Click(object sender, RoutedEventArgs e)
        {
            intentarConexion();
        }

        private void mostrarErrorConexion()
        {
            iconoConexion.Visibility = Visibility.Visible;
            labelConexion.Visibility = Visibility.Visible;
            btnReintentarConexión.Visibility = Visibility.Visible;
            iconoReintentar.Visibility = Visibility.Visible;
            labelReintentar.Visibility = Visibility.Visible;

            socketCliente.Close();
        }

        private void ocultarErrorConexion()
        {
            iconoConexion.Visibility = Visibility.Hidden;
            labelConexion.Visibility = Visibility.Hidden;
            btnReintentarConexión.Visibility = Visibility.Hidden;
            iconoReintentar.Visibility = Visibility.Hidden;
            labelReintentar.Visibility = Visibility.Hidden;
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

        public void recibirMensaje(string mensajeRecibido, string usuarioEmisor)
        {
            //Crear user control del mensaje

            GridChatRecibido.Children.Add(new MensajeChat(posicionMensaje, mensajeRecibido, usuarioEmisor));

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
            socketCliente = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            LoopConnect();
            socketCliente.BeginReceive(receivedBuf, 0, receivedBuf.Length, SocketFlags.None, new AsyncCallback(ReceiveData), socketCliente);
            
            
            byte[] buffer = Encoding.Default.GetBytes(serializarMensaje("", false, false));
            socketCliente.Send(buffer);
        }


        //Actualizar valores del mensaje y serializarlos
        public string serializarMensaje(string contenidoMensaje, bool isMensajeConexion, bool isMensaje)
        {
            EnvioMensajeChat mensajeChat = new EnvioMensajeChat(contenidoMensaje, usuarioEmisor, delegacionEmisor, isMensaje, false);
            return Newtonsoft.Json.JsonConvert.SerializeObject(mensajeChat).ToString();
        }

        byte[] receivedBuf = new byte[1024];

        //Recibir información del servidor
        private void ReceiveData(IAsyncResult ar)
        {
            try
            {
                Socket socket = (Socket)ar.AsyncState;
                int received = socket.EndReceive(ar);
                byte[] dataBuf = new byte[received];
                Array.Copy(receivedBuf, dataBuf, received);

                mensaje = (Encoding.Default.GetString(dataBuf));

            }
            catch (SocketException)
            {
                this.Dispatcher.Invoke(() =>
                {
                    mostrarErrorConexion();
                });
                return;
            }


            // Manejo de conectados y nuevos mensajes

            string objetoSerializado = mensaje;

            envioMensajeChat = Newtonsoft.Json.JsonConvert.DeserializeObject<EnvioMensajeChat>(mensaje);

            if (envioMensajeChat.contenidoMensaje == null)
            {
                ClienteConectado cc = Newtonsoft.Json.JsonConvert.DeserializeObject<ClienteConectado>(objetoSerializado);
                //Llenar lista con nombre de usuario y delegacion
                listaConectados = new List<string>();
                for (int i = 0; i < cc.usuariosEmisores.Count; i++)
                {
                    listaConectados.Add(cc.usuariosEmisores[i] + " (" + cc.delegacionesEmisores[i] + ")");
                }

                this.Dispatcher.Invoke(() =>
                {
                    if (cc.usuariosEmisores != null)
                    {
                        dataGridUsuariosConectados.ItemsSource = listaConectados;

                    }
                });
            }
            else
            {
                this.Dispatcher.Invoke(() =>
                {
                    recibirMensaje(envioMensajeChat.contenidoMensaje, envioMensajeChat.usuarioEmisor);
                });
            }

            



            try
            {
                socketCliente.BeginReceive(receivedBuf, 0, receivedBuf.Length, SocketFlags.None, new AsyncCallback(ReceiveData), socketCliente);
            }
            catch (SocketException)
            {
                return;
            }

        }



        private void LoopConnect()
        {

            while (!socketCliente.Connected)
            {
                try
                {
                    socketCliente.Connect(IPAddress.Loopback, 100);
                }
                catch (Exception)
                {
                    return;
                }
            }
        }

        private void envioMsj(string msj)
        {
            if (socketCliente.Connected)
            {
                byte[] buffer = Encoding.Default.GetBytes(serializarMensaje(msj, false, true));
                socketCliente.Send(buffer);
            }
        }
    }
}