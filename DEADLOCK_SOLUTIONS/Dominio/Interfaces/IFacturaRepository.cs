using DEADLOCK_SOLUTIONS.Dominio.Entidades;

namespace DEADLOCK_SOLUTIONS.Dominio.Interfaces;

public interface IFacturaRepository
{
    void Add(Factura factura);
}
