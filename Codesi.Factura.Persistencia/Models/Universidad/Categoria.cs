using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Codesi.Factura.Persistencia.Models.Universidad;

[Table("categorias")]
[Index("Nombre", Name = "UQ__categori__72AFBCC6B3D04AB2", IsUnique = true)]
public partial class Categoria
{
    [Key]
    [Column("id_categoria")]
    public int IdCategoria { get; set; }

    [Column("nombre")]
    [StringLength(100)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [Column("activo")]
    public bool Activo { get; set; }

    [InverseProperty("IdCategoriaNavigation")]
    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
