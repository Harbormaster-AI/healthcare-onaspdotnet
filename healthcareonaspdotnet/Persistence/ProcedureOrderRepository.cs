
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class ProcedureOrderRepository : IProcedureOrderRepository
{
    private readonly ApplicationDbContext _db;

    public ProcedureOrderRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ProcedureOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ProcedureOrders
            .Include(x => x.Order)
            .Include(x => x.Facility)
            .Include(x => x.Procedure)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ProcedureOrder>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ProcedureOrders
            .AsNoTracking()
            .Include(x => x.Order)
            .Include(x => x.Facility)
            .Include(x => x.Procedure)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ProcedureOrder procedureOrder, CancellationToken cancellationToken)
    {
        _db.ProcedureOrders.Add(procedureOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ProcedureOrder procedureOrder, CancellationToken cancellationToken)
    {
        _db.ProcedureOrders.Update(procedureOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ProcedureOrder procedureOrder, CancellationToken cancellationToken)
    {
        _db.ProcedureOrders.Remove(procedureOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
