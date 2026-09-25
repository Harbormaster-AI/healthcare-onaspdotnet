
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class DiagnosisRepository : IDiagnosisRepository
{
    private readonly ApplicationDbContext _db;

    public DiagnosisRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Diagnosis?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Diagnosiss
            .Include(x => x.Encounter)
            .Include(x => x.Patient)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Diagnosis>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Diagnosiss
            .AsNoTracking()
            .Include(x => x.Encounter)
            .Include(x => x.Patient)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Diagnosis diagnosis, CancellationToken cancellationToken)
    {
        _db.Diagnosiss.Add(diagnosis);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Diagnosis diagnosis, CancellationToken cancellationToken)
    {
        _db.Diagnosiss.Update(diagnosis);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Diagnosis diagnosis, CancellationToken cancellationToken)
    {
        _db.Diagnosiss.Remove(diagnosis);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
