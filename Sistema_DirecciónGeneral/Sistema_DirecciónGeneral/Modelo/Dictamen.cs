//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sistema_DirecciónGeneral.Modelo
{
    using System;
    using System.Collections.Generic;
    
    public partial class Dictamen
    {
        public string folio { get; set; }
        public string descripcion { get; set; }
        public string responsable { get; set; }
        public Nullable<System.DateTime> fechaHora { get; set; }
        public Nullable<int> idUsuario { get; set; }
        public int idReporte { get; set; }
    
        public virtual Reporte Reporte { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
