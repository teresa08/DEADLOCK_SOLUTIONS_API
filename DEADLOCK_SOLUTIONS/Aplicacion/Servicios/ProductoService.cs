using DEADLOCK_SOLUTIONS.Aplicacion.DTOS;
using DEADLOCK_SOLUTIONS.Aplicacion.Interfaces;
using DEADLOCK_SOLUTIONS.Aplicacion.Mapeos;
using DEADLOCK_SOLUTIONS.Dominio.Entidades;
using DEADLOCK_SOLUTIONS.Dominio.Interfaces;

namespace DEADLOCK_SOLUTIONS.Aplicacion.Servicios;

public class ProductoService(IProductoRepository productoRepository, IUnitOfWork unitOfWork) : IProductoService
{
    public async Task<Response<List<ProductoDto>>> ListarAsync()
    {
        var productos = await productoRepository.GetAllAsync();

        return Response<List<ProductoDto>>.Success(productos.Select(producto => producto.ToDto()).ToList());
    }

    public async Task<Response<ProductoDto>> RegistrarAsync(CrearProductoDto dto)
    {
        var producto = new Producto
        {
            Nombre = dto.Nombre.Trim(),
            Precio = dto.Precio,
            Stock = dto.Stock
        };

        productoRepository.Add(producto);
        await unitOfWork.SaveChangesAsync();

        return Response<ProductoDto>.Success(producto.ToDto(), "Producto registrado.", StatusCodes.Status201Created);
    }
}
