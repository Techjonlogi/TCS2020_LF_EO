using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_DirecciónGeneral.Classes
{
    class EnvioMensajeChat
    {

        public string contenidoMensaje { get; set; }
        public string usuarioEmisor { get; set; }
        public string delegacionEmisor { get; set; }
        public bool isMensaje { get; set; }
        public bool isReporte { get; set; }
        public string reporteVerificacion { get; set; }

        public EnvioMensajeChat()
        {

        }

        public EnvioMensajeChat(string contenidoMensaje, string usuarioEmisor, string delegacionEmisor, bool isMensaje, bool isReporte)
        {
            this.contenidoMensaje = contenidoMensaje;
            this.usuarioEmisor = usuarioEmisor;
            this.delegacionEmisor = delegacionEmisor;
            this.isMensaje = isMensaje;
            this.isReporte = isReporte;
            reporteVerificacion = " ";
        }

    }
}