using DEADLOCK_SOLUTIONS.Dominio.Entidades;
using DEADLOCK_SOLUTIONS.Dominio.Interfaces;
using DEADLOCK_SOLUTIONS.Infraestructura.Data;

namespace DEADLOCK_SOLUTIONS.Infraestructura.Repository;

public class FacturaRepository(DeadlockContext context) : IFacturaRepository
{
    public void Add(Factura factura) => context.Facturas.Add(factura);
}
