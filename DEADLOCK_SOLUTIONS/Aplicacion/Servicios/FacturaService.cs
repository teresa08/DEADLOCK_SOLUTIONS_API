using DEADLOCK_SOLUTIONS.Aplicacion.DTOS;
using DEADLOCK_SOLUTIONS.Aplicacion.Interfaces;
using DEADLOCK_SOLUTIONS.Aplicacion.Mapeos;
using DEADLOCK_SOLUTIONS.Dominio.Entidades;
using DEADLOCK_SOLUTIONS.Dominio.Interfaces;

namespace DEADLOCK_SOLUTIONS.Aplicacion.Servicios;

public class FacturaService(
    IOrdenRepository ordenRepository,
    IFacturaRepository facturaRepository,
    IUnitOfWork unitOfWork) : IFacturaService
{
    public async Task<Response<FacturaDto>> GenerarAsync(int ordenId)
    {
        var orden = await ordenRepository.GetByIdAsync(ordenId);

        if (orden is null)
        {
            return OrdenNoEncontrada();
        }

        if (orden.Factura is not null)
        {
            return Response<FacturaDto>.Error("La orden ya tiene una factura.", StatusCodes.Status409Conflict);
        }

        if (orden.Items.Count == 0)
        {
            return Response<FacturaDto>.Error("No se puede facturar una orden sin items.", StatusCodes.Status409Conflict);
        }

        var factura = new Factura
        {
            Numero = $"FAC-{orden.Id:D6}",
            Total = orden.Total,
            Orden = orden
        };

        facturaRepository.Add(factura);
        await unitOfWork.SaveChangesAsync();

        return Response<FacturaDto>.Success(factura.ToDto(), "Factura generada.", StatusCodes.Status201Created);
    }

    public async Task<Response<FacturaDto>> ObtenerPorOrdenAsync(int ordenId)
    {
        var orden = await ordenRepository.GetByIdAsync(ordenId);

        if (orden is null)
        {
            return OrdenNoEncontrada();
        }

        return orden.Factura is null
            ? Response<FacturaDto>.Error("La orden aun no tiene factura.", StatusCodes.Status404NotFound)
            : Response<FacturaDto>.Success(orden.Factura.ToDto());
    }

    private static Response<FacturaDto> OrdenNoEncontrada() =>
        Response<FacturaDto>.Error("Orden no encontrada.", StatusCodes.Status404NotFound);
}
