using System.ComponentModel.DataAnnotations;

namespace DEADLOCK_SOLUTIONS.Aplicacion.DTOS;

public class CrearProductoDto
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [MaxLength(50)]
    public string Nombre { get; set; } = null!;

    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a cero.")]
    public decimal Precio { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
    public int Stock { get; set; }
}
