using healthcareonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class CarePlanRepository : ICarePlanRepository
{
    private readonly ApplicationDbContext _db;

    public CarePlanRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<CarePlan?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.CarePlans
            .Include(x => x.Patient)
            .Include(x => x.CareTeam)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<CarePlan>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.CarePlans
            .AsNoTracking()
            .Include(x => x.Patient)
            .Include(x => x.CareTeam)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(CarePlan carePlan, CancellationToken cancellationToken)
    {
        _db.CarePlans.Add(carePlan);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(CarePlan carePlan, CancellationToken cancellationToken)
    {
        _db.CarePlans.Update(carePlan);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(CarePlan carePlan, CancellationToken cancellationToken)
    {
        _db.CarePlans.Remove(carePlan);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
