
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class ConditionRepository : IConditionRepository
{
    private readonly ApplicationDbContext _db;

    public ConditionRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Condition?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Conditions
            .Include(x => x.Patient)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Condition>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Conditions
            .AsNoTracking()
            .Include(x => x.Patient)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Condition condition, CancellationToken cancellationToken)
    {
        _db.Conditions.Add(condition);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Condition condition, CancellationToken cancellationToken)
    {
        _db.Conditions.Update(condition);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Condition condition, CancellationToken cancellationToken)
    {
        _db.Conditions.Remove(condition);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
