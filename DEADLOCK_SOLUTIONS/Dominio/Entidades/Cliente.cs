namespace DEADLOCK_SOLUTIONS.Dominio.Entidades;

public class Cliente
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public string? Direccion { get; set; }

    public DateTime Fecha { get; set; } = DateTime.UtcNow;

    public ICollection<Orden> Ordenes { get; set; } = new List<Orden>();
}
