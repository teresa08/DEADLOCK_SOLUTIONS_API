using System.ComponentModel.DataAnnotations;

namespace DEADLOCK_SOLUTIONS.Aplicacion.DTOS;

public class CrearOrdenItemDto
{
    [Range(1, int.MaxValue, ErrorMessage = "El producto es obligatorio.")]
    public int ProductoId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a cero.")]
    public int Cantidad { get; set; }
}
