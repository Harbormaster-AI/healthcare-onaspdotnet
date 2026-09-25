
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class DischargeRepository : IDischargeRepository
{
    private readonly ApplicationDbContext _db;

    public DischargeRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Discharge?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Discharges
            .Include(x => x.Encounter)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Discharge>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Discharges
            .AsNoTracking()
            .Include(x => x.Encounter)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Discharge discharge, CancellationToken cancellationToken)
    {
        _db.Discharges.Add(discharge);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Discharge discharge, CancellationToken cancellationToken)
    {
        _db.Discharges.Update(discharge);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Discharge discharge, CancellationToken cancellationToken)
    {
        _db.Discharges.Remove(discharge);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
