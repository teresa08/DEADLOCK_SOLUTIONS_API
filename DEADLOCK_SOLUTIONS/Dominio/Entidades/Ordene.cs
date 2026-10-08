using System;
using System.Collections.Generic;

namespace DEADLOCK_SOLUTIONS.Dominio.Entidades;

public partial class Ordene
{
    public int Id { get; set; }

    public DateTime? Fecha { get; set; }

    public int IdCliente { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual ICollection<OrdenesItem> OrdenesItems { get; set; } = new List<OrdenesItem>();
}
