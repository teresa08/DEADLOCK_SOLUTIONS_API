namespace DEADLOCK_SOLUTIONS.Dominio.Entidades;

public class OrdenItem
{
    public int Id { get; set; }

    public int OrdenId { get; set; }

    public int ProductoId { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public Orden Orden { get; set; } = null!;

    public Producto Producto { get; set; } = null!;

    public decimal Subtotal => Cantidad * PrecioUnitario;
}
