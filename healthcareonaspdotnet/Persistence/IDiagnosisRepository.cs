using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Persistence;

public interface IDiagnosisRepository
{
    Task<Diagnosis?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Diagnosis>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Diagnosis diagnosis, CancellationToken cancellationToken);
    Task UpdateAsync(Diagnosis diagnosis, CancellationToken cancellationToken);
    Task DeleteAsync(Diagnosis diagnosis, CancellationToken cancellationToken);


}
