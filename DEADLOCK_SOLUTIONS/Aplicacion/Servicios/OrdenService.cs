using DEADLOCK_SOLUTIONS.Aplicacion.DTOS;
using DEADLOCK_SOLUTIONS.Aplicacion.Interfaces;
using DEADLOCK_SOLUTIONS.Aplicacion.Mapeos;
using DEADLOCK_SOLUTIONS.Dominio.Entidades;
using DEADLOCK_SOLUTIONS.Dominio.Interfaces;

namespace DEADLOCK_SOLUTIONS.Aplicacion.Servicios;

public class OrdenService(
    IOrdenRepository ordenRepository,
    IClienteRepository clienteRepository,
    IProductoRepository productoRepository,
    IUnitOfWork unitOfWork) : IOrdenService
{
    public async Task<Response<List<OrdenDto>>> ListarAsync()
    {
        var ordenes = await ordenRepository.GetAllAsync();

        return Response<List<OrdenDto>>.Success(ordenes.Select(orden => orden.ToDto()).ToList());
    }

    public async Task<Response<OrdenDto>> ObtenerAsync(int id)
    {
        var orden = await ordenRepository.GetByIdAsync(id);

        return orden is null ? OrdenNoEncontrada() : Response<OrdenDto>.Success(orden.ToDto());
    }

    public async Task<Response<OrdenDto>> CrearAsync(CrearOrdenDto dto)
    {
        var productoIds = dto.Items.Select(item => item.ProductoId).Distinct().ToList();

        if (productoIds.Count != dto.Items.Count)
        {
            return Response<OrdenDto>.Error("La orden no puede repetir productos.", StatusCodes.Status400BadRequest);
        }

        var cliente = await clienteRepository.GetByIdAsync(dto.ClienteId);

        if (cliente is null)
        {
            return Response<OrdenDto>.Error("Cliente no encontrado.", StatusCodes.Status404NotFound);
        }

        var productos = await productoRepository.GetByIdsAsync(productoIds);

        if (productos.Count != productoIds.Count)
        {
            return Response<OrdenDto>.Error("Uno o mas productos no existen.", StatusCodes.Status404NotFound);
        }

        var orden = new Orden { Cliente = cliente };

        foreach (var itemDto in dto.Items)
        {
            var producto = productos.First(p => p.Id == itemDto.ProductoId);

            if (producto.Stock < itemDto.Cantidad)
            {
                return StockInsuficiente(producto);
            }

            producto.Stock -= itemDto.Cantidad;
            orden.Items.Add(new OrdenItem
            {
                Producto = producto,
                Cantidad = itemDto.Cantidad,
                PrecioUnitario = producto.Precio
            });
        }

        ordenRepository.Add(orden);
        await unitOfWork.SaveChangesAsync();

        return Response<OrdenDto>.Success(orden.ToDto(), "Orden creada.", StatusCodes.Status201Created);
    }

    public async Task<Response<OrdenDto>> ActualizarCantidadItemAsync(int ordenId, int itemId, ActualizarCantidadDto dto)
    {
        var orden = await ordenRepository.GetByIdAsync(ordenId);

        if (orden is null)
        {
            return OrdenNoEncontrada();
        }

        if (orden.Factura is not null)
        {
            return OrdenFacturada();
        }

        var item = orden.Items.FirstOrDefault(i => i.Id == itemId);

        if (item is null)
        {
            return ItemNoEncontrado();
        }

        var diferencia = dto.Cantidad - item.Cantidad;

        if (item.Producto.Stock < diferencia)
        {
            return StockInsuficiente(item.Producto);
        }

        item.Producto.Stock -= diferencia;
        item.Cantidad = dto.Cantidad;
        await unitOfWork.SaveChangesAsync();

        return Response<OrdenDto>.Success(orden.ToDto(), "Cantidad actualizada.");
    }

    public async Task<Response<OrdenDto>> EliminarItemAsync(int ordenId, int itemId)
    {
        var orden = await ordenRepository.GetByIdAsync(ordenId);

        if (orden is null)
        {
            return OrdenNoEncontrada();
        }

        if (orden.Factura is not null)
        {
            return OrdenFacturada();
        }

        var item = orden.Items.FirstOrDefault(i => i.Id == itemId);

        if (item is null)
        {
            return ItemNoEncontrado();
        }

        item.Producto.Stock += item.Cantidad;
        orden.Items.Remove(item);
        ordenRepository.RemoveItem(item);
        await unitOfWork.SaveChangesAsync();

        return Response<OrdenDto>.Success(orden.ToDto(), "Item eliminado.");
    }

    private static Response<OrdenDto> OrdenNoEncontrada() =>
        Response<OrdenDto>.Error("Orden no encontrada.", StatusCodes.Status404NotFound);

    private static Response<OrdenDto> ItemNoEncontrado() =>
        Response<OrdenDto>.Error("El item no pertenece a la orden.", StatusCodes.Status404NotFound);

    private static Response<OrdenDto> OrdenFacturada() =>
        Response<OrdenDto>.Error("La orden ya fue facturada y no puede modificarse.", StatusCodes.Status409Conflict);

    private static Response<OrdenDto> StockInsuficiente(Producto producto) =>
        Response<OrdenDto>.Error($"Stock insuficiente para {producto.Nombre}. Disponible: {producto.Stock}.", StatusCodes.Status409Conflict);
}
