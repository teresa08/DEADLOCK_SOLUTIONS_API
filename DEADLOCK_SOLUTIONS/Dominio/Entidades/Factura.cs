using System;
using System.Collections.Generic;

namespace DEADLOCK_SOLUTIONS.Dominio.Entidades;

public partial class Factura
{
    public int Id { get; set; }

    public string Numero { get; set; } = null!;

    public DateTime? Fecha { get; set; }


}
