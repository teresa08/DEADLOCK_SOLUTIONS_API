using DEADLOCK_SOLUTIONS.Dominio.Entidades;
using DEADLOCK_SOLUTIONS.Dominio.Interfaces;
using DEADLOCK_SOLUTIONS.Infraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace DEADLOCK_SOLUTIONS.Infraestructura.Repository;

public class OrdenRepository(DeadlockContext context) : IOrdenRepository
{
    public Task<List<Orden>> GetAllAsync() =>
        QueryWithDetails().AsNoTracking().OrderByDescending(orden => orden.Id).ToListAsync();

    public Task<Orden?> GetByIdAsync(int id) =>
        QueryWithDetails().FirstOrDefaultAsync(orden => orden.Id == id);

    public void Add(Orden orden) => context.Ordenes.Add(orden);

    public void RemoveItem(OrdenItem item) => context.OrdenItems.Remove(item);

    private IQueryable<Orden> QueryWithDetails() =>
        context.Ordenes
            .Include(orden => orden.Cliente)
            .Include(orden => orden.Factura)
            .Include(orden => orden.Items)
            .ThenInclude(item => item.Producto);
}
