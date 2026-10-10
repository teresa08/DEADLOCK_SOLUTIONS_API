using DEADLOCK_SOLUTIONS.Aplicacion.DTOS;

namespace DEADLOCK_SOLUTIONS.Aplicacion.Interfaces;

public interface IProductoService
{
    Task<Response<List<ProductoDto>>> ListarAsync();

    Task<Response<ProductoDto>> RegistrarAsync(CrearProductoDto dto);
}
