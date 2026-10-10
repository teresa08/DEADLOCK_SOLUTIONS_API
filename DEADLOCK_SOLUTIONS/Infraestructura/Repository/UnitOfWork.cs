using DEADLOCK_SOLUTIONS.Dominio.Interfaces;
using DEADLOCK_SOLUTIONS.Infraestructura.Data;

namespace DEADLOCK_SOLUTIONS.Infraestructura.Repository;

public class UnitOfWork(DeadlockContext context) : IUnitOfWork
{
    public Task SaveChangesAsync() => context.SaveChangesAsync();
}
