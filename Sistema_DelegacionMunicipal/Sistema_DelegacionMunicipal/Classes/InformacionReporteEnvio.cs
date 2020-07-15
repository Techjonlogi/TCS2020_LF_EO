using Sistema_DelegacionMunicipal.Modelo;
using System.Collections.Generic;

namespace Sistema_DelegacionMunicipal.Classes
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
