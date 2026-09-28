using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Codesi.Factura.Persistencia.Models.Universidad;

[Table("productos")]
[Index("CodigoPrincipal", Name = "UQ__producto__C7E95A92EA248E6F", IsUnique = true)]
public partial class Producto
{
    [Key]
    [Column("id_producto")]
    public int IdProducto { get; set; }

    [Column("codigo_principal")]
    [StringLength(50)]
    [Unicode(false)]
    public string CodigoPrincipal { get; set; } = null!;

    [Column("nombre")]
    [StringLength(150)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [Column("precio_unitario", TypeName = "decimal(10, 2)")]
    public decimal PrecioUnitario { get; set; }

    [Column("stock")]
    public int Stock { get; set; }

    [Column("tarifa_iva", TypeName = "decimal(5, 2)")]
    public decimal TarifaIva { get; set; }

    [Column("id_categoria")]
    public int? IdCategoria { get; set; }

    [Column("activo")]
    public bool Activo { get; set; }

    [Column("fecha_creacion", TypeName = "datetime")]
    public DateTime FechaCreacion { get; set; }

    [InverseProperty("IdProductoNavigation")]
    public virtual ICollection<FacturaDetalle> FacturaDetalles { get; set; } = new List<FacturaDetalle>();

    [ForeignKey("IdCategoria")]
    [InverseProperty("Productos")]
    public virtual Categoria? IdCategoriaNavigation { get; set; }
}
