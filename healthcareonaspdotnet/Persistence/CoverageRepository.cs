using healthcareonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class CoverageRepository : ICoverageRepository
{
    private readonly ApplicationDbContext _db;

    public CoverageRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Coverage?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Coverages
            .Include(x => x.Patient)
            .Include(x => x.Plan)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Coverage>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Coverages
            .AsNoTracking()
            .Include(x => x.Patient)
            .Include(x => x.Plan)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Coverage coverage, CancellationToken cancellationToken)
    {
        _db.Coverages.Add(coverage);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Coverage coverage, CancellationToken cancellationToken)
    {
        _db.Coverages.Update(coverage);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Coverage coverage, CancellationToken cancellationToken)
    {
        _db.Coverages.Remove(coverage);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
