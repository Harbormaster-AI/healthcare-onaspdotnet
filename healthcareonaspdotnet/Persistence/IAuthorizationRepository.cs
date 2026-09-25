using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Persistence;

public interface IAuthorizationRepository
{
    Task<Authorization?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Authorization>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Authorization authorization, CancellationToken cancellationToken);
    Task UpdateAsync(Authorization authorization, CancellationToken cancellationToken);
    Task DeleteAsync(Authorization authorization, CancellationToken cancellationToken);


}
