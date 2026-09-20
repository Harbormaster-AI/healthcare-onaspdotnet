using healthcareonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class FacilityRepository : IFacilityRepository
{
    private readonly ApplicationDbContext _db;

    public FacilityRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Facility?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Facilitys
            .Include(x => x.HealthSystem)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Facility>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Facilitys
            .AsNoTracking()
            .Include(x => x.HealthSystem)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Facility facility, CancellationToken cancellationToken)
    {
        _db.Facilitys.Add(facility);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Facility facility, CancellationToken cancellationToken)
    {
        _db.Facilitys.Update(facility);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Facility facility, CancellationToken cancellationToken)
    {
        _db.Facilitys.Remove(facility);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
