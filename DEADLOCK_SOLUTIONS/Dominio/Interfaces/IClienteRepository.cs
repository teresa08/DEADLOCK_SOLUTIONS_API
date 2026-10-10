using DEADLOCK_SOLUTIONS.Dominio.Entidades;

namespace DEADLOCK_SOLUTIONS.Dominio.Interfaces;

public interface IClienteRepository
{
    Task<List<Cliente>> GetAllAsync();

    Task<Cliente?> GetByIdAsync(int id);

    void Add(Cliente cliente);
}
