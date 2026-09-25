
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class AuthorizationRepository : IAuthorizationRepository
{
    private readonly ApplicationDbContext _db;

    public AuthorizationRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Authorization?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Authorizations
            .Include(x => x.Coverage)
            .Include(x => x.Order)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Authorization>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Authorizations
            .AsNoTracking()
            .Include(x => x.Coverage)
            .Include(x => x.Order)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Authorization authorization, CancellationToken cancellationToken)
    {
        _db.Authorizations.Add(authorization);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Authorization authorization, CancellationToken cancellationToken)
    {
        _db.Authorizations.Update(authorization);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Authorization authorization, CancellationToken cancellationToken)
    {
        _db.Authorizations.Remove(authorization);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
