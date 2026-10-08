using System;
using System.Collections.Generic;

namespace DEADLOCK_SOLUTIONS.Dominio.Entidades;

public partial class Cliente
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string? Numero { get; set; }

    public string? Correo { get; set; }

    public string? Direccion { get; set; }

    public DateTime? Fecha { get; set; }

    public virtual ICollection<Ordene> Ordenes { get; set; } = new List<Ordene>();
}
