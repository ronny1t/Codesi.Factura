using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Codesi.Factura.Persistencia.Models.Universidad;

[Table("facturas")]
[Index("ClaveAcceso", Name = "UQ__facturas__46F7790DCF4A7E3D", IsUnique = true)]
[Index("Establecimiento", "PuntoEmision", "Secuencial", Name = "UQ_factura_comprobante", IsUnique = true)]
public partial class Factura
{
    [Key]
    [Column("id_factura")]
    public int IdFactura { get; set; }

    [Column("establecimiento")]
    [StringLength(3)]
    [Unicode(false)]
    public string Establecimiento { get; set; } = null!;

    [Column("punto_emision")]
    [StringLength(3)]
    [Unicode(false)]
    public string PuntoEmision { get; set; } = null!;

    [Column("secuencial")]
    [StringLength(9)]
    [Unicode(false)]
    public string Secuencial { get; set; } = null!;

    [Column("clave_acceso")]
    [StringLength(49)]
    [Unicode(false)]
    public string? ClaveAcceso { get; set; }

    [Column("fecha_emision", TypeName = "datetime")]
    public DateTime FechaEmision { get; set; }

    [Column("id_cliente")]
    public int IdCliente { get; set; }

    [Column("subtotal_sin_impuestos", TypeName = "decimal(10, 2)")]
    public decimal SubtotalSinImpuestos { get; set; }

    [Column("total_descuento", TypeName = "decimal(10, 2)")]
    public decimal TotalDescuento { get; set; }

    [Column("subtotal_iva", TypeName = "decimal(10, 2)")]
    public decimal SubtotalIva { get; set; }

    [Column("propina", TypeName = "decimal(10, 2)")]
    public decimal Propina { get; set; }

    [Column("importe_total", TypeName = "decimal(10, 2)")]
    public decimal ImporteTotal { get; set; }

    [Column("estado_sri")]
    [StringLength(20)]
    [Unicode(false)]
    public string EstadoSri { get; set; } = null!;

    [InverseProperty("IdFacturaNavigation")]
    public virtual ICollection<FacturaDetalle> FacturaDetalles { get; set; } = new List<FacturaDetalle>();

    [InverseProperty("IdFacturaNavigation")]
    public virtual ICollection<FacturaPago> FacturaPagos { get; set; } = new List<FacturaPago>();

    [ForeignKey("IdCliente")]
    [InverseProperty("Facturas")]
    public virtual Cliente IdClienteNavigation { get; set; } = null!;
}
