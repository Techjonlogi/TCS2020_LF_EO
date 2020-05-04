using Sistema_DelegacionMunicipal.ViewController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_DelegacionMunicipal.Sockets
{
    class SocketCliente
    {
        public bool Conectar(string mensaje)
        {
            bool isEnviado = false;
            Socket socketCliente = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint direccionConexion = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);
            try
            {
                socketCliente.Connect(direccionConexion);

                byte[] msjEnviar = Encoding.Default.GetBytes(mensaje);
                socketCliente.Send(msjEnviar, 0, msjEnviar.Length, 0);

                isEnviado = true;
            }
            catch (Exception e)
            {

            }
            return isEnviado;
        }

    }
}
