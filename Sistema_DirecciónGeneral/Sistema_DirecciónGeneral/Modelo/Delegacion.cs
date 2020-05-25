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
    
    public partial class Delegacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Delegacion()
        {
            this.Reportes = new HashSet<Reporte>();
            this.Usuarios = new HashSet<Usuario>();
        }
    
        public int idDelegacion { get; set; }
        public string nombre { get; set; }
        public string calle { get; set; }
        public string colonia { get; set; }
        public string codigoPostal { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
    
        public virtual Municipio Municipio { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reporte> Reportes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
