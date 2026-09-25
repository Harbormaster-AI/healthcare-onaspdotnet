using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Persistence;

public interface IConditionRepository
{
    Task<Condition?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Condition>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Condition condition, CancellationToken cancellationToken);
    Task UpdateAsync(Condition condition, CancellationToken cancellationToken);
    Task DeleteAsync(Condition condition, CancellationToken cancellationToken);


}
