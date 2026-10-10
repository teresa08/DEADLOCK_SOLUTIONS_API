using DEADLOCK_SOLUTIONS.Aplicacion.DTOS;
using DEADLOCK_SOLUTIONS.Aplicacion.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DEADLOCK_SOLUTIONS.Controllers;

[ApiController]
[Route("api/productos")]
public class ProductosController(IProductoService productoService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var response = await productoService.ListarAsync();

        return StatusCode(response.Status, response);
    }

    [HttpPost]
    public async Task<IActionResult> Registrar(CrearProductoDto dto)
    {
        var response = await productoService.RegistrarAsync(dto);

        return StatusCode(response.Status, response);
    }
}
