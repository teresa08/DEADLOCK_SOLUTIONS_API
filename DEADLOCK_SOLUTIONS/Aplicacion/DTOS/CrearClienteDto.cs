using System.ComponentModel.DataAnnotations;

namespace DEADLOCK_SOLUTIONS.Aplicacion.DTOS;

public class CrearClienteDto
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [MaxLength(50)]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = "El apellido es obligatorio.")]
    [MaxLength(100)]
    public string Apellido { get; set; } = null!;

    [MaxLength(20)]
    public string? Telefono { get; set; }

    [EmailAddress(ErrorMessage = "El correo no es valido.")]
    [MaxLength(100)]
    public string? Correo { get; set; }

    [MaxLength(100)]
    public string? Direccion { get; set; }
}
