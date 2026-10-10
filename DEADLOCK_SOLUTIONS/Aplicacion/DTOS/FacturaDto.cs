namespace DEADLOCK_SOLUTIONS.Aplicacion.DTOS;

public record FacturaDto(int Id, string Numero, DateTime Fecha, int OrdenId, ClienteDto Cliente, List<OrdenItemDto> Items, decimal Total);
