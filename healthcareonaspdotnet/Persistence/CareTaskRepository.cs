
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class CareTaskRepository : ICareTaskRepository
{
    private readonly ApplicationDbContext _db;

    public CareTaskRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<CareTask?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.CareTasks
            .Include(x => x.CarePlan)
            .Include(x => x.AssignedTo)
            .Include(x => x.Encounter)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<CareTask>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.CareTasks
            .AsNoTracking()
            .Include(x => x.CarePlan)
            .Include(x => x.AssignedTo)
            .Include(x => x.Encounter)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(CareTask careTask, CancellationToken cancellationToken)
    {
        _db.CareTasks.Add(careTask);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(CareTask careTask, CancellationToken cancellationToken)
    {
        _db.CareTasks.Update(careTask);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(CareTask careTask, CancellationToken cancellationToken)
    {
        _db.CareTasks.Remove(careTask);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
