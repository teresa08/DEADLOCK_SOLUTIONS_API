using DEADLOCK_SOLUTIONS.Aplicacion.DTOS;

namespace DEADLOCK_SOLUTIONS.Aplicacion.Interfaces;

public interface IFacturaService
{
    Task<Response<FacturaDto>> GenerarAsync(int ordenId);

    Task<Response<FacturaDto>> ObtenerPorOrdenAsync(int ordenId);
}
