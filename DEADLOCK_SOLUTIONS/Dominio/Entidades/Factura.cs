namespace DEADLOCK_SOLUTIONS.Dominio.Entidades;

public class Factura
{
    public int Id { get; set; }

    public string Numero { get; set; } = null!;

    public DateTime Fecha { get; set; } = DateTime.UtcNow;

    public decimal Total { get; set; }

    public int OrdenId { get; set; }

    public Orden Orden { get; set; } = null!;
}
