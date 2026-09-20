using healthcareonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class ClinicianRepository : IClinicianRepository
{
    private readonly ApplicationDbContext _db;

    public ClinicianRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Clinician?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Clinicians
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Clinician>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Clinicians
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Clinician clinician, CancellationToken cancellationToken)
    {
        _db.Clinicians.Add(clinician);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Clinician clinician, CancellationToken cancellationToken)
    {
        _db.Clinicians.Update(clinician);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Clinician clinician, CancellationToken cancellationToken)
    {
        _db.Clinicians.Remove(clinician);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
