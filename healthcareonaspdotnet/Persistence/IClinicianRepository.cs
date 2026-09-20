using healthcareonaspdotnet.Domain;

namespace healthcareonaspdotnet.Persistence;

public interface IClinicianRepository
{
    Task<Clinician?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Clinician>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Clinician clinician, CancellationToken cancellationToken);
    Task UpdateAsync(Clinician clinician, CancellationToken cancellationToken);
    Task DeleteAsync(Clinician clinician, CancellationToken cancellationToken);
}
