using DEADLOCK_SOLUTIONS.Aplicacion.DTOS;

namespace DEADLOCK_SOLUTIONS.Aplicacion.Interfaces;

public interface IOrdenService
{
    Task<Response<List<OrdenDto>>> ListarAsync();

    Task<Response<OrdenDto>> ObtenerAsync(int id);

    Task<Response<OrdenDto>> CrearAsync(CrearOrdenDto dto);

    Task<Response<OrdenDto>> ActualizarCantidadItemAsync(int ordenId, int itemId, ActualizarCantidadDto dto);

    Task<Response<OrdenDto>> EliminarItemAsync(int ordenId, int itemId);
}
