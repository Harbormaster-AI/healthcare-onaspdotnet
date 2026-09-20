using healthcareonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class HealthSystemRepository : IHealthSystemRepository
{
    private readonly ApplicationDbContext _db;

    public HealthSystemRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<HealthSystem?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.HealthSystems
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<HealthSystem>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.HealthSystems
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(HealthSystem healthSystem, CancellationToken cancellationToken)
    {
        _db.HealthSystems.Add(healthSystem);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(HealthSystem healthSystem, CancellationToken cancellationToken)
    {
        _db.HealthSystems.Update(healthSystem);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(HealthSystem healthSystem, CancellationToken cancellationToken)
    {
        _db.HealthSystems.Remove(healthSystem);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
