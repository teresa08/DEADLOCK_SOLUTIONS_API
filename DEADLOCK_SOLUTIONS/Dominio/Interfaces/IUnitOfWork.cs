namespace DEADLOCK_SOLUTIONS.Dominio.Interfaces;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}
