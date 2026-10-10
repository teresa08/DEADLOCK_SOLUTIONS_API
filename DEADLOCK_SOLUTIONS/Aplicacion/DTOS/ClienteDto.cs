namespace DEADLOCK_SOLUTIONS.Aplicacion.DTOS;

public record ClienteDto(int Id, string Nombre, string Apellido, string? Telefono, string? Correo, string? Direccion);
