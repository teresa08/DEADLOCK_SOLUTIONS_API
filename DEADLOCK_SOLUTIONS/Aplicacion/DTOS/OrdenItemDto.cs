namespace DEADLOCK_SOLUTIONS.Aplicacion.DTOS;

public record OrdenItemDto(int Id, int ProductoId, string Producto, int Cantidad, decimal PrecioUnitario, decimal Subtotal);
