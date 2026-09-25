
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class SoftwareUpdateRepository : ISoftwareUpdateRepository
{
    private readonly ApplicationDbContext _db;

    public SoftwareUpdateRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<SoftwareUpdate?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.SoftwareUpdates
            .Include(x => x.Device)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<SoftwareUpdate>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.SoftwareUpdates
            .AsNoTracking()
            .Include(x => x.Device)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(SoftwareUpdate softwareUpdate, CancellationToken cancellationToken)
    {
        _db.SoftwareUpdates.Add(softwareUpdate);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(SoftwareUpdate softwareUpdate, CancellationToken cancellationToken)
    {
        _db.SoftwareUpdates.Update(softwareUpdate);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(SoftwareUpdate softwareUpdate, CancellationToken cancellationToken)
    {
        _db.SoftwareUpdates.Remove(softwareUpdate);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
