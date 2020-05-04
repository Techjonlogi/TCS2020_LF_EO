using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_DelegacionMunicipal.Sockets
{
    class SocketServer
    {
        public void Conectar(object sender, DoWorkEventArgs e)
        {
            Socket socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint direccion = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);
            socketServer.Bind(direccion);
            socketServer.Listen(2);

            (sender as BackgroundWorker).ReportProgress(10, "Socket escuchando.");

            Socket socketClienteRemoto = socketServer.Accept();

            string infoCliente = "";
            string mensaje = "";

            (sender as BackgroundWorker).ReportProgress(50, infoCliente);

            byte[] byteRemoto = new byte[255];
            int datos = socketClienteRemoto.Receive(byteRemoto, 0, byteRemoto.Length, 0);
            Array.Resize(ref byteRemoto, datos);

            mensaje = Encoding.Default.GetString(byteRemoto);

            socketServer.Close();

        }

    }
}
