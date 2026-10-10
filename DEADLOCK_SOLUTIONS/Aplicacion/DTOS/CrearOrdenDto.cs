using System.ComponentModel.DataAnnotations;

namespace DEADLOCK_SOLUTIONS.Aplicacion.DTOS;

public class CrearOrdenDto
{
    public const int MinimoProductos = 2;

    [Range(1, int.MaxValue, ErrorMessage = "El cliente es obligatorio.")]
    public int ClienteId { get; set; }

    [Required]
    [MinLength(MinimoProductos, ErrorMessage = "La orden debe tener al menos dos productos.")]
    public List<CrearOrdenItemDto> Items { get; set; } = [];
}
