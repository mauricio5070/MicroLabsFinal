//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MicroLabsFinal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Factura
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Factura()
        {
            this.Detalle = new HashSet<Detalle>();
        }
    
        public int numeroFactura { get; set; }
        public string codigoFactura { get; set; }
        public System.DateTime fechaCompra { get; set; }
        public int monedaID { get; set; }
        public decimal tipoCambio { get; set; }
        public int pagoID { get; set; }
        public int codigoProveedor { get; set; }
        public Nullable<decimal> total { get; set; }
    
        public virtual Moneda Moneda { get; set; }
        public virtual modoPago modoPago { get; set; }
        public virtual Proveedor Proveedor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Detalle> Detalle { get; set; }
    }
}
