using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;


namespace Servidor
{


    public partial class MainWindow : Window
    {
        // Lista de sockets para los clientes y socket para el servidor

        private byte[] buffer = new byte[1024];
        private List<SocketServer> socketsClientes { get; set; }

        List<string> listaClientesConectados = new List<string>();
        List<string> listaClienteDelegaciones = new List<string>();
        List<string> listaDirecciones = new List<string>();

        private Socket socketServidor = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        ClienteConectado cc = new ClienteConectado();

        string cliente = "";

        public MainWindow()
        {
            InitializeComponent();
            socketsClientes = new List<SocketServer>();
            iniciarServidor();
        }



        private void iniciarServidor()
        {
            lbServer.Content = "Servidor Iniciado";
            socketServidor.Bind(new IPEndPoint(IPAddress.Any, 100));
            socketServidor.Listen(1);

            //Aceptar intento de conexión
            socketServidor.BeginAccept(new AsyncCallback(AceptarCallBack), null);
        }






         // Escuchar conexiones y sockets para clientes
        private void AceptarCallBack(IAsyncResult ar)
        {
            //Aceptar intento de conexión
            Socket socket = socketServidor.EndAccept(ar);

            //Añadir cliente nuevo a la lista
            socketsClientes.Add(new SocketServer(socket));
            
            //Comenzar a recibir datos 

            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
            socketServidor.BeginAccept(new AsyncCallback(AceptarCallBack), null);
        }








        //Recibir información de un socket
        private void ReceiveCallback(IAsyncResult ar)
        {

            Socket socket = (Socket)ar.AsyncState;

            if (socket.Connected)
            {
                int received;
                try
                {
                    received = socket.EndReceive(ar);
                }
                catch (Exception)
                {

                    for (int i = 0; i < socketsClientes.Count; i++)
                    {
                        if (socketsClientes[i].socket.RemoteEndPoint.ToString().Equals(socket.RemoteEndPoint.ToString()))
                        {
                            socketsClientes.RemoveAt(i);

                            cc.delegacionesEmisores.RemoveAt(i);
                            cc.direccionesUsuario.RemoveAt(i);
                            cc.usuariosEmisores.RemoveAt(i);

                            string ccActualizado = Newtonsoft.Json.JsonConvert.SerializeObject(cc).ToString();
                            enviarListaATodos(ccActualizado);
                        }
                    }

                    return;
                }
                if (received != 0)
                {
                    byte[] dataBuf = new byte[received];
                    Array.Copy(buffer, dataBuf, received);
                    string texto = Encoding.Default.GetString(dataBuf);

                    
                    

                    string respuesta = string.Empty;


                    for (int i = 0; i < socketsClientes.Count; i++)
                    {
                        if (socket.RemoteEndPoint.ToString().Equals(socketsClientes[i].socket.RemoteEndPoint.ToString()))
                        {
                            cliente = socketsClientes[i].socket.RemoteEndPoint.ToString();
                            socketsClientes[i].nombreCliente = socketsClientes[i].socket.RemoteEndPoint.ToString();

                        }
                    }


                    //Deserializar objeto

                    string reporteRecibido = texto;
                    EnvioMensajeChat mensajeChat = Newtonsoft.Json.JsonConvert.DeserializeObject<EnvioMensajeChat>(texto);
                    

                    //Añadir nuevo conectado a la lista si es que es mensaje de conexión
                    if (!mensajeChat.isMensaje && !mensajeChat.isReporte)
                    {
                        listaClientesConectados.Add(mensajeChat.usuarioEmisor);
                        listaClienteDelegaciones.Add(mensajeChat.delegacionEmisor);
                        listaDirecciones.Add(cliente);
                        

                        cc = new ClienteConectado(listaClientesConectados, listaClienteDelegaciones, listaDirecciones);
                        

                        //Enviar lista de conectados a todos los clientes
                        respuesta = Newtonsoft.Json.JsonConvert.SerializeObject(cc).ToString();

                        enviarListaATodos(respuesta);
                    }

                    //Enviar reporte a dirección general en caso de ser un reporte
                    if (mensajeChat.isReporte)
                    {
                        reenviarAGeneral(reporteRecibido);
                    }

                    //Reenviar respuesta con el contenido del mensaje si es que es mensaje de chat

                    if (mensajeChat.isMensaje)
                    {
                        reenviarATodos(texto, cliente);
                    }
                    


                }
                else
                {
                    for (int i = 0; i < socketsClientes.Count; i++)
                    {
                        if (socketsClientes[i].socket.RemoteEndPoint.ToString().Equals(socket.RemoteEndPoint.ToString()))
                        {
                            socketsClientes.RemoveAt(i);

                            cc.delegacionesEmisores.RemoveAt(i);
                            cc.direccionesUsuario.RemoveAt(i);
                            cc.usuariosEmisores.RemoveAt(i);

                            string ccActualizado = Newtonsoft.Json.JsonConvert.SerializeObject(cc).ToString();
                            enviarListaATodos(ccActualizado);
                        }
                    }
                }
            }
            try
            {
                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
            }
            catch (SocketException)
            {

            }
        }





        public void enviarListaATodos(string listaSerializada)
        {

            for (int i = 0; i < socketsClientes.Count; i++)
            {
                EnviarInfo(socketsClientes[i].socket, listaSerializada);
            }

        }


        public void reenviarATodos(string mensaje, string cliente)
        {
           
            for (int i = 0; i < socketsClientes.Count; i++)
            {
                //Verificar que el que envió el mensaje no lo reciba
                if (cliente != socketsClientes[i].nombreCliente)
                    EnviarInfo(socketsClientes[i].socket, mensaje);

            }
            
        }

        public void reenviarAGeneral(string reporteSerializado)
        {
            for (int i = 0; i < socketsClientes.Count; i++)
            {
                if (listaClienteDelegaciones[i] == "Dir General")
                {
                    EnviarInfo(socketsClientes[i].socket, reporteSerializado);
                }
            }
        }


        void EnviarInfo(Socket socket, string mensaje)
        {
            try
            {
                byte[] data = Encoding.Default.GetBytes(mensaje);
                socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), socket);
                socketServidor.BeginAccept(new AsyncCallback(AceptarCallBack), null);
            }
            catch(SocketException){

            }
            
        }

        private void SendCallback(IAsyncResult AR)
        {
            Socket socket = (Socket)AR.AsyncState;
            socket.EndSend(AR);
        }
    }

}
