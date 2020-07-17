using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_DirecciónGeneral.Classes
{
    class InformacionReporteEnvio
    {
        public bool isMensaje { get; set; }
        public bool isReporte { get; set; }
        public string reporteVerificacion { get; set; }
        public InformacionReporteEnvio()
        {

        }

        public InformacionReporteEnvio(bool isMensaje, bool isReporte, string reporteVerificacion)
        {
            this.isMensaje = isMensaje;
            this.isReporte = isReporte;
            this.reporteVerificacion = reporteVerificacion;
        }
    }
}
