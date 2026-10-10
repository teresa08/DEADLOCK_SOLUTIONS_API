using DEADLOCK_SOLUTIONS.Dominio.Entidades;

namespace DEADLOCK_SOLUTIONS.Dominio.Interfaces;

public interface IProductoRepository
{
    Task<List<Producto>> GetAllAsync();

    Task<List<Producto>> GetByIdsAsync(IEnumerable<int> ids);

    void Add(Producto producto);
}
