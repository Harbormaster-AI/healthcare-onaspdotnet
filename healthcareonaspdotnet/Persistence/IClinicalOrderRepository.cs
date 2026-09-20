using healthcareonaspdotnet.Domain;

namespace healthcareonaspdotnet.Persistence;

public interface IClinicalOrderRepository
{
    Task<ClinicalOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ClinicalOrder>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ClinicalOrder clinicalOrder, CancellationToken cancellationToken);
    Task UpdateAsync(ClinicalOrder clinicalOrder, CancellationToken cancellationToken);
    Task DeleteAsync(ClinicalOrder clinicalOrder, CancellationToken cancellationToken);
}
