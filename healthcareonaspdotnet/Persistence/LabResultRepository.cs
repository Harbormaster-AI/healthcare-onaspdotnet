using healthcareonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class LabResultRepository : ILabResultRepository
{
    private readonly ApplicationDbContext _db;

    public LabResultRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<LabResult?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.LabResults
            .Include(x => x.LaboratoryOrder)
            .Include(x => x.Laboratory)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<LabResult>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.LabResults
            .AsNoTracking()
            .Include(x => x.LaboratoryOrder)
            .Include(x => x.Laboratory)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(LabResult labResult, CancellationToken cancellationToken)
    {
        _db.LabResults.Add(labResult);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(LabResult labResult, CancellationToken cancellationToken)
    {
        _db.LabResults.Update(labResult);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(LabResult labResult, CancellationToken cancellationToken)
    {
        _db.LabResults.Remove(labResult);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
