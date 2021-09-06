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
    
    public partial class Detalle
    {
        public int idDetalle { get; set; }
        public int numeroFactura { get; set; }
        public string codigoProducto { get; set; }
        public int cantidad { get; set; }
        public decimal precioUnitario { get; set; }
        public Nullable<decimal> montoIVA { get; set; }
        public Nullable<decimal> subTotal { get; set; }
        public int idIVA { get; set; }
    
        public virtual Factura Factura { get; set; }
        public virtual IVA IVA { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
