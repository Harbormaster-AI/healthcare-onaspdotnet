using healthcareonaspdotnet.Domain;

namespace healthcareonaspdotnet.Persistence;

public interface ICareTeamRepository
{
    Task<CareTeam?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<CareTeam>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(CareTeam careTeam, CancellationToken cancellationToken);
    Task UpdateAsync(CareTeam careTeam, CancellationToken cancellationToken);
    Task DeleteAsync(CareTeam careTeam, CancellationToken cancellationToken);
}
