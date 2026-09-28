using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Codesi.Factura.Persistencia.Models.Universidad;

[Table("clientes")]
[Index("Identificacion", Name = "UQ__clientes__C196DEC70A6B966D", IsUnique = true)]
public partial class Cliente
{
    [Key]
    [Column("id_cliente")]
    public int IdCliente { get; set; }

    [Column("tipo_identificacion")]
    [StringLength(20)]
    [Unicode(false)]
    public string TipoIdentificacion { get; set; } = null!;

    [Column("identificacion")]
    [StringLength(20)]
    [Unicode(false)]
    public string Identificacion { get; set; } = null!;

    [Column("razon_social")]
    [StringLength(200)]
    [Unicode(false)]
    public string RazonSocial { get; set; } = null!;

    [Column("direccion")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Direccion { get; set; }

    [Column("telefono")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Telefono { get; set; }

    [Column("email")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("activo")]
    public bool Activo { get; set; }

    [Column("fecha_creacion", TypeName = "datetime")]
    public DateTime FechaCreacion { get; set; }

    [InverseProperty("IdClienteNavigation")]
    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}
