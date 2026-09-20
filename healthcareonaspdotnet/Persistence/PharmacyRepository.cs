using healthcareonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class PharmacyRepository : IPharmacyRepository
{
    private readonly ApplicationDbContext _db;

    public PharmacyRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Pharmacy?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Pharmacys
            .Include(x => x.Facility)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Pharmacy>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Pharmacys
            .AsNoTracking()
            .Include(x => x.Facility)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Pharmacy pharmacy, CancellationToken cancellationToken)
    {
        _db.Pharmacys.Add(pharmacy);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Pharmacy pharmacy, CancellationToken cancellationToken)
    {
        _db.Pharmacys.Update(pharmacy);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Pharmacy pharmacy, CancellationToken cancellationToken)
    {
        _db.Pharmacys.Remove(pharmacy);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
