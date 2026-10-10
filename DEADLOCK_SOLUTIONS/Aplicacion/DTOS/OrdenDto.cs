namespace DEADLOCK_SOLUTIONS.Aplicacion.DTOS;

public record OrdenDto(int Id, DateTime Fecha, ClienteDto Cliente, List<OrdenItemDto> Items, decimal Total, bool Facturada);
