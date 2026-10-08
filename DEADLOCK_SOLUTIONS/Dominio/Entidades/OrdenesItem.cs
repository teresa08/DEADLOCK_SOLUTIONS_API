using System;
using System.Collections.Generic;

namespace DEADLOCK_SOLUTIONS.Dominio.Entidades;

public partial class OrdenesItem
{
    public int Id { get; set; }

    public DateTime? Fecha { get; set; }

    public int IdOrdenes { get; set; }

    public int IdProducto { get; set; }

    public virtual Ordene IdOrdenesNavigation { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
