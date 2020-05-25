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
        public Socket socket { get; set; }

        public string nombreCliente { get; set; }


        public SocketServer(Socket socket)
        {
            this.socket = socket;
        }

    }


}
