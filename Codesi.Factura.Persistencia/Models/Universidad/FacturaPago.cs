using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Codesi.Factura.Persistencia.Models.Universidad;

[Table("factura_pagos")]
public partial class FacturaPago
{
    [Key]
    [Column("id_pago")]
    public int IdPago { get; set; }

    [Column("id_factura")]
    public int IdFactura { get; set; }

    [Column("forma_pago")]
    [StringLength(50)]
    [Unicode(false)]
    public string FormaPago { get; set; } = null!;

    [Column("total", TypeName = "decimal(10, 2)")]
    public decimal Total { get; set; }

    [ForeignKey("IdFactura")]
    [InverseProperty("FacturaPagos")]
    public virtual Factura IdFacturaNavigation { get; set; } = null!;
}
