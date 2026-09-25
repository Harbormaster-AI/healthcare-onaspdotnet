
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class ObservationRepository : IObservationRepository
{
    private readonly ApplicationDbContext _db;

    public ObservationRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Observation?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Observations
            .Include(x => x.Encounter)
            .Include(x => x.Patient)
            .Include(x => x.Device)
            .Include(x => x.LabResult)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Observation>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Observations
            .AsNoTracking()
            .Include(x => x.Encounter)
            .Include(x => x.Patient)
            .Include(x => x.Device)
            .Include(x => x.LabResult)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Observation observation, CancellationToken cancellationToken)
    {
        _db.Observations.Add(observation);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Observation observation, CancellationToken cancellationToken)
    {
        _db.Observations.Update(observation);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Observation observation, CancellationToken cancellationToken)
    {
        _db.Observations.Remove(observation);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
