using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Codesi.Factura.Persistencia.Models.Universidad;

[Table("factura_detalles")]
public partial class FacturaDetalle
{
    [Key]
    [Column("id_detalle")]
    public int IdDetalle { get; set; }

    [Column("id_factura")]
    public int IdFactura { get; set; }

    [Column("id_producto")]
    public int IdProducto { get; set; }

    [Column("cantidad")]
    public int Cantidad { get; set; }

    [Column("precio_unitario", TypeName = "decimal(10, 2)")]
    public decimal PrecioUnitario { get; set; }

    [Column("descuento", TypeName = "decimal(10, 2)")]
    public decimal Descuento { get; set; }

    [Column("subtotal", TypeName = "decimal(10, 2)")]
    public decimal Subtotal { get; set; }

    [Column("valor_iva", TypeName = "decimal(10, 2)")]
    public decimal ValorIva { get; set; }

    [Column("total", TypeName = "decimal(10, 2)")]
    public decimal Total { get; set; }

    [ForeignKey("IdFactura")]
    [InverseProperty("FacturaDetalles")]
    public virtual Factura IdFacturaNavigation { get; set; } = null!;

    [ForeignKey("IdProducto")]
    [InverseProperty("FacturaDetalles")]
    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
