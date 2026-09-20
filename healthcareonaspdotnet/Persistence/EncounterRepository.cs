using healthcareonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class EncounterRepository : IEncounterRepository
{
    private readonly ApplicationDbContext _db;

    public EncounterRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Encounter?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Encounters
            .Include(x => x.Patient)
            .Include(x => x.Clinician)
            .Include(x => x.Facility)
            .Include(x => x.Appointment)
            .Include(x => x.Admission)
            .Include(x => x.Discharge)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Encounter>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Encounters
            .AsNoTracking()
            .Include(x => x.Patient)
            .Include(x => x.Clinician)
            .Include(x => x.Facility)
            .Include(x => x.Appointment)
            .Include(x => x.Admission)
            .Include(x => x.Discharge)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Encounter encounter, CancellationToken cancellationToken)
    {
        _db.Encounters.Add(encounter);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Encounter encounter, CancellationToken cancellationToken)
    {
        _db.Encounters.Update(encounter);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Encounter encounter, CancellationToken cancellationToken)
    {
        _db.Encounters.Remove(encounter);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
