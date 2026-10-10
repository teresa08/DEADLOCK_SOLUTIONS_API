using DEADLOCK_SOLUTIONS.Dominio.Entidades;

namespace DEADLOCK_SOLUTIONS.Dominio.Interfaces;

public interface IOrdenRepository
{
    Task<List<Orden>> GetAllAsync();

    Task<Orden?> GetByIdAsync(int id);

    void Add(Orden orden);

    void RemoveItem(OrdenItem item);
}
