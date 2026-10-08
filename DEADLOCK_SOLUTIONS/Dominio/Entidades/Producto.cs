using System;
using System.Collections.Generic;

namespace DEADLOCK_SOLUTIONS.Dominio.Entidades;

public partial class Producto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int? Cantidad { get; set; }

    public decimal? Precio { get; set; }

    public int? Stock { get; set; }

    public virtual ICollection<OrdenesItem> OrdenesItems { get; set; } = new List<OrdenesItem>();
}
