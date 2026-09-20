using healthcareonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class ClinicalOrderRepository : IClinicalOrderRepository
{
    private readonly ApplicationDbContext _db;

    public ClinicalOrderRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ClinicalOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ClinicalOrders
            .Include(x => x.Patient)
            .Include(x => x.Encounter)
            .Include(x => x.OrderingClinician)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ClinicalOrder>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ClinicalOrders
            .AsNoTracking()
            .Include(x => x.Patient)
            .Include(x => x.Encounter)
            .Include(x => x.OrderingClinician)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ClinicalOrder clinicalOrder, CancellationToken cancellationToken)
    {
        _db.ClinicalOrders.Add(clinicalOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ClinicalOrder clinicalOrder, CancellationToken cancellationToken)
    {
        _db.ClinicalOrders.Update(clinicalOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ClinicalOrder clinicalOrder, CancellationToken cancellationToken)
    {
        _db.ClinicalOrders.Remove(clinicalOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
