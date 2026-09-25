
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class ProcedureRepository : IProcedureRepository
{
    private readonly ApplicationDbContext _db;

    public ProcedureRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Procedure?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Procedures
            .Include(x => x.Encounter)
            .Include(x => x.Performer)
            .Include(x => x.ProcedureOrder)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Procedure>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Procedures
            .AsNoTracking()
            .Include(x => x.Encounter)
            .Include(x => x.Performer)
            .Include(x => x.ProcedureOrder)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Procedure procedure, CancellationToken cancellationToken)
    {
        _db.Procedures.Add(procedure);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Procedure procedure, CancellationToken cancellationToken)
    {
        _db.Procedures.Update(procedure);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Procedure procedure, CancellationToken cancellationToken)
    {
        _db.Procedures.Remove(procedure);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
