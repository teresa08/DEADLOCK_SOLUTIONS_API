using DEADLOCK_SOLUTIONS.Aplicacion.DTOS;
using DEADLOCK_SOLUTIONS.Dominio.Entidades;

namespace DEADLOCK_SOLUTIONS.Aplicacion.Mapeos;

public static class MapeoExtensions
{
    public static ClienteDto ToDto(this Cliente cliente) =>
        new(cliente.Id, cliente.Nombre, cliente.Apellido, cliente.Telefono, cliente.Correo, cliente.Direccion);

    public static ProductoDto ToDto(this Producto producto) =>
        new(producto.Id, producto.Nombre, producto.Precio, producto.Stock);

    public static OrdenItemDto ToDto(this OrdenItem item) =>
        new(item.Id, item.ProductoId, item.Producto.Nombre, item.Cantidad, item.PrecioUnitario, item.Subtotal);

    public static OrdenDto ToDto(this Orden orden) =>
        new(orden.Id, orden.Fecha, orden.Cliente.ToDto(), orden.Items.ToDtoList(), orden.Total, orden.Factura is not null);

    public static FacturaDto ToDto(this Factura factura) =>
        new(factura.Id, factura.Numero, factura.Fecha, factura.OrdenId, factura.Orden.Cliente.ToDto(), factura.Orden.Items.ToDtoList(), factura.Total);

    private static List<OrdenItemDto> ToDtoList(this IEnumerable<OrdenItem> items) =>
        items.Select(item => item.ToDto()).ToList();
}
