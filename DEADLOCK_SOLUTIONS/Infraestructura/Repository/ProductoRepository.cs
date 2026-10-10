using DEADLOCK_SOLUTIONS.Dominio.Entidades;
using DEADLOCK_SOLUTIONS.Dominio.Interfaces;
using DEADLOCK_SOLUTIONS.Infraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace DEADLOCK_SOLUTIONS.Infraestructura.Repository;

public class ProductoRepository(DeadlockContext context) : IProductoRepository
{
    public Task<List<Producto>> GetAllAsync() =>
        context.Productos.AsNoTracking().OrderBy(producto => producto.Nombre).ToListAsync();

    public Task<List<Producto>> GetByIdsAsync(IEnumerable<int> ids) =>
        context.Productos.Where(producto => ids.Contains(producto.Id)).ToListAsync();

    public void Add(Producto producto) => context.Productos.Add(producto);
}
