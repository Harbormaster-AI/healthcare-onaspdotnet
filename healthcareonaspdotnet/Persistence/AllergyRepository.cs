
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class AllergyRepository : IAllergyRepository
{
    private readonly ApplicationDbContext _db;

    public AllergyRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Allergy?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Allergys
            .Include(x => x.Patient)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Allergy>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Allergys
            .AsNoTracking()
            .Include(x => x.Patient)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Allergy allergy, CancellationToken cancellationToken)
    {
        _db.Allergys.Add(allergy);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Allergy allergy, CancellationToken cancellationToken)
    {
        _db.Allergys.Update(allergy);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Allergy allergy, CancellationToken cancellationToken)
    {
        _db.Allergys.Remove(allergy);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
