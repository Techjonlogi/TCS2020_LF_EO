using Servidor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
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

namespace Servidor
{


    public partial class MainWindow : Window
    {
        // Lista de sockets para los clientes y socket para el servidor

        private byte[] buffer = new byte[1024];
        private List<SocketServer> socketsClientes { get; set; }

        private Socket socketServidor = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

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






        //Escuchar conexiones y sockets para clientes
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


                    respuesta = "" + texto;
                    reenviarATodos(respuesta, cliente);


                }
                else
                {
                    for (int i = 0; i < socketsClientes.Count; i++)
                    {
                        if (socketsClientes[i].socket.RemoteEndPoint.ToString().Equals(socket.RemoteEndPoint.ToString()))
                        {
                            socketsClientes.RemoveAt(i);
                        }
                    }
                }
            }
            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
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


        void EnviarInfo(Socket socket, string mensaje)
        {
            byte[] data = Encoding.Default.GetBytes(mensaje);
            socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), socket);
            socketServidor.BeginAccept(new AsyncCallback(AceptarCallBack), null);
        }

        private void SendCallback(IAsyncResult AR)
        {
            Socket socket = (Socket)AR.AsyncState;
            socket.EndSend(AR);
        }
    }

}
