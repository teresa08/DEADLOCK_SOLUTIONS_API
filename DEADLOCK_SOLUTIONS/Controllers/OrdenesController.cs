using DEADLOCK_SOLUTIONS.Aplicacion.DTOS;
using DEADLOCK_SOLUTIONS.Aplicacion.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DEADLOCK_SOLUTIONS.Controllers;

[ApiController]
[Route("api/ordenes")]
public class OrdenesController(IOrdenService ordenService, IFacturaService facturaService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var response = await ordenService.ListarAsync();

        return StatusCode(response.Status, response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Obtener(int id)
    {
        var response = await ordenService.ObtenerAsync(id);

        return StatusCode(response.Status, response);
    }

    [HttpPost]
    public async Task<IActionResult> Crear(CrearOrdenDto dto)
    {
        var response = await ordenService.CrearAsync(dto);

        return StatusCode(response.Status, response);
    }

    [HttpPut("{id:int}/items/{itemId:int}")]
    public async Task<IActionResult> ActualizarCantidadItem(int id, int itemId, ActualizarCantidadDto dto)
    {
        var response = await ordenService.ActualizarCantidadItemAsync(id, itemId, dto);

        return StatusCode(response.Status, response);
    }

    [HttpDelete("{id:int}/items/{itemId:int}")]
    public async Task<IActionResult> EliminarItem(int id, int itemId)
    {
        var response = await ordenService.EliminarItemAsync(id, itemId);

        return StatusCode(response.Status, response);
    }

    [HttpPost("{id:int}/factura")]
    public async Task<IActionResult> GenerarFactura(int id)
    {
        var response = await facturaService.GenerarAsync(id);

        return StatusCode(response.Status, response);
    }

    [HttpGet("{id:int}/factura")]
    public async Task<IActionResult> ObtenerFactura(int id)
    {
        var response = await facturaService.ObtenerPorOrdenAsync(id);

        return StatusCode(response.Status, response);
    }
}
