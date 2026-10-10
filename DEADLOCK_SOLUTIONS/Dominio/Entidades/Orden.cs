namespace DEADLOCK_SOLUTIONS.Dominio.Entidades;

public class Orden
{
    public int Id { get; set; }

    public DateTime Fecha { get; set; } = DateTime.UtcNow;

    public int ClienteId { get; set; }

    public Cliente Cliente { get; set; } = null!;

    public Factura? Factura { get; set; }

    public ICollection<OrdenItem> Items { get; set; } = new List<OrdenItem>();

    public decimal Total => Items.Sum(item => item.Subtotal);
}
