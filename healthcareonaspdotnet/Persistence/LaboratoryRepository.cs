using healthcareonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class LaboratoryRepository : ILaboratoryRepository
{
    private readonly ApplicationDbContext _db;

    public LaboratoryRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Laboratory?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Laboratorys
            .Include(x => x.Facility)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Laboratory>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Laboratorys
            .AsNoTracking()
            .Include(x => x.Facility)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Laboratory laboratory, CancellationToken cancellationToken)
    {
        _db.Laboratorys.Add(laboratory);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Laboratory laboratory, CancellationToken cancellationToken)
    {
        _db.Laboratorys.Update(laboratory);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Laboratory laboratory, CancellationToken cancellationToken)
    {
        _db.Laboratorys.Remove(laboratory);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
