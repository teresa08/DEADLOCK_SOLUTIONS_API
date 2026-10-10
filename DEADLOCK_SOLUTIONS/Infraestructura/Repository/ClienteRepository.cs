using DEADLOCK_SOLUTIONS.Dominio.Entidades;
using DEADLOCK_SOLUTIONS.Dominio.Interfaces;
using DEADLOCK_SOLUTIONS.Infraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace DEADLOCK_SOLUTIONS.Infraestructura.Repository;

public class ClienteRepository(DeadlockContext context) : IClienteRepository
{
    public Task<List<Cliente>> GetAllAsync() =>
        context.Clientes.AsNoTracking().OrderBy(cliente => cliente.Nombre).ToListAsync();

    public Task<Cliente?> GetByIdAsync(int id) =>
        context.Clientes.FirstOrDefaultAsync(cliente => cliente.Id == id);

    public void Add(Cliente cliente) => context.Clientes.Add(cliente);
}
