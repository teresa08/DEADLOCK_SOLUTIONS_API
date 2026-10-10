using DEADLOCK_SOLUTIONS.Aplicacion.DTOS;

namespace DEADLOCK_SOLUTIONS.Aplicacion.Interfaces;

public interface IClienteService
{
    Task<Response<List<ClienteDto>>> ListarAsync();

    Task<Response<ClienteDto>> ObtenerAsync(int id);

    Task<Response<ClienteDto>> RegistrarAsync(CrearClienteDto dto);
}
