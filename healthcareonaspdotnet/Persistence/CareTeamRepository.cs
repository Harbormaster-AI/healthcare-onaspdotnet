using healthcareonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class CareTeamRepository : ICareTeamRepository
{
    private readonly ApplicationDbContext _db;

    public CareTeamRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<CareTeam?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.CareTeams
            .Include(x => x.Department)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<CareTeam>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.CareTeams
            .AsNoTracking()
            .Include(x => x.Department)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(CareTeam careTeam, CancellationToken cancellationToken)
    {
        _db.CareTeams.Add(careTeam);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(CareTeam careTeam, CancellationToken cancellationToken)
    {
        _db.CareTeams.Update(careTeam);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(CareTeam careTeam, CancellationToken cancellationToken)
    {
        _db.CareTeams.Remove(careTeam);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
