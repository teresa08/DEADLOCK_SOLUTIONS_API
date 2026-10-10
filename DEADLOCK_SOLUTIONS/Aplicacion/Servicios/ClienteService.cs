using DEADLOCK_SOLUTIONS.Aplicacion.DTOS;
using DEADLOCK_SOLUTIONS.Aplicacion.Interfaces;
using DEADLOCK_SOLUTIONS.Aplicacion.Mapeos;
using DEADLOCK_SOLUTIONS.Dominio.Entidades;
using DEADLOCK_SOLUTIONS.Dominio.Interfaces;

namespace DEADLOCK_SOLUTIONS.Aplicacion.Servicios;

public class ClienteService(IClienteRepository clienteRepository, IUnitOfWork unitOfWork) : IClienteService
{
    public async Task<Response<List<ClienteDto>>> ListarAsync()
    {
        var clientes = await clienteRepository.GetAllAsync();

        return Response<List<ClienteDto>>.Success(clientes.Select(cliente => cliente.ToDto()).ToList());
    }

    public async Task<Response<ClienteDto>> ObtenerAsync(int id)
    {
        var cliente = await clienteRepository.GetByIdAsync(id);

        return cliente is null
            ? Response<ClienteDto>.Error("Cliente no encontrado.", StatusCodes.Status404NotFound)
            : Response<ClienteDto>.Success(cliente.ToDto());
    }

    public async Task<Response<ClienteDto>> RegistrarAsync(CrearClienteDto dto)
    {
        var cliente = new Cliente
        {
            Nombre = dto.Nombre.Trim(),
            Apellido = dto.Apellido.Trim(),
            Telefono = dto.Telefono,
            Correo = dto.Correo,
            Direccion = dto.Direccion
        };

        clienteRepository.Add(cliente);
        await unitOfWork.SaveChangesAsync();

        return Response<ClienteDto>.Success(cliente.ToDto(), "Cliente registrado.", StatusCodes.Status201Created);
    }
}
