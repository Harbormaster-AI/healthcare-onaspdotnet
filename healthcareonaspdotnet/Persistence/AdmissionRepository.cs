
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class AdmissionRepository : IAdmissionRepository
{
    private readonly ApplicationDbContext _db;

    public AdmissionRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Admission?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Admissions
            .Include(x => x.Encounter)
            .Include(x => x.Facility)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Admission>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Admissions
            .AsNoTracking()
            .Include(x => x.Encounter)
            .Include(x => x.Facility)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Admission admission, CancellationToken cancellationToken)
    {
        _db.Admissions.Add(admission);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Admission admission, CancellationToken cancellationToken)
    {
        _db.Admissions.Update(admission);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Admission admission, CancellationToken cancellationToken)
    {
        _db.Admissions.Remove(admission);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
