using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Servidor
{
    class SocketServer
    {

        private byte[] buffer = new byte[1024];
        public List<SocketS> socketsCliente { get; set; }

        List<string> nombres = new List<string>();

        private Socket socketServidor = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


        string cliente = "";

        public void Conectar(object sender, DoWorkEventArgs e)
        {
            Socket socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint direccion = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);
            socketServer.Bind(direccion);
            socketServer.Listen(2);

            (sender as BackgroundWorker).ReportProgress(10, "Socket escuchando.");
            Console.WriteLine("Escuchando...");

            Socket socketClienteRemoto = socketServer.Accept();

            IPEndPoint clienteRem = (IPEndPoint)socketClienteRemoto.RemoteEndPoint;
            string infoCliente = $"Cliente remoto conectado con IP: {clienteRem.Address} en el puerto {clienteRem.Port}";

            (sender as BackgroundWorker).ReportProgress(50, infoCliente);

            byte[] byteRemoto = new byte[90000000];
            int datos = socketClienteRemoto.Receive(byteRemoto, 0, byteRemoto.Length, 0);
            Array.Resize(ref byteRemoto, datos);

            long nomArchivo = DateTime.Now.ToFileTime();
            string filePath = "C:\\Users\\normal\\Desktop\\" + nomArchivo + ".png";
            File.WriteAllBytes(filePath, byteRemoto);

            socketServer.Close();

            e.Result = filePath;
        }
    }
















}
