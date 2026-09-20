using healthcareonaspdotnet.Domain;

namespace healthcareonaspdotnet.Persistence;

public interface ICoverageRepository
{
    Task<Coverage?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Coverage>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Coverage coverage, CancellationToken cancellationToken);
    Task UpdateAsync(Coverage coverage, CancellationToken cancellationToken);
    Task DeleteAsync(Coverage coverage, CancellationToken cancellationToken);
}
