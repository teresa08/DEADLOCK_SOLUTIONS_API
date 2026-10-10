using DEADLOCK_SOLUTIONS.Aplicacion.DTOS;
using DEADLOCK_SOLUTIONS.Aplicacion.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DEADLOCK_SOLUTIONS.Controllers;

[ApiController]
[Route("api/clientes")]
public class ClientesController(IClienteService clienteService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var response = await clienteService.ListarAsync();

        return StatusCode(response.Status, response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Obtener(int id)
    {
        var response = await clienteService.ObtenerAsync(id);

        return StatusCode(response.Status, response);
    }

    [HttpPost]
    public async Task<IActionResult> Registrar(CrearClienteDto dto)
    {
        var response = await clienteService.RegistrarAsync(dto);

        return StatusCode(response.Status, response);
    }
}
