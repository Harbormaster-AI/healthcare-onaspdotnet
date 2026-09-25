using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Persistence;

public interface ICareTaskRepository
{
    Task<CareTask?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<CareTask>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(CareTask careTask, CancellationToken cancellationToken);
    Task UpdateAsync(CareTask careTask, CancellationToken cancellationToken);
    Task DeleteAsync(CareTask careTask, CancellationToken cancellationToken);


}
