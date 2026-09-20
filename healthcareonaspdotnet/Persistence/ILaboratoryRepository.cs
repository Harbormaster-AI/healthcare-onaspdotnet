using healthcareonaspdotnet.Domain;

namespace healthcareonaspdotnet.Persistence;

public interface ILaboratoryRepository
{
    Task<Laboratory?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Laboratory>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Laboratory laboratory, CancellationToken cancellationToken);
    Task UpdateAsync(Laboratory laboratory, CancellationToken cancellationToken);
    Task DeleteAsync(Laboratory laboratory, CancellationToken cancellationToken);
}
