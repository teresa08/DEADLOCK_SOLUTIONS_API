using System.ComponentModel.DataAnnotations;

namespace DEADLOCK_SOLUTIONS.Aplicacion.DTOS;

public class ActualizarCantidadDto
{
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a cero.")]
    public int Cantidad { get; set; }
}
