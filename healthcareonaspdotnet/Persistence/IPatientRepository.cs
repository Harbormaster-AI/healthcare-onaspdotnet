using healthcareonaspdotnet.Domain;

namespace healthcareonaspdotnet.Persistence;

public interface IPatientRepository
{
    Task<Patient?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Patient>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Patient patient, CancellationToken cancellationToken);
    Task UpdateAsync(Patient patient, CancellationToken cancellationToken);
    Task DeleteAsync(Patient patient, CancellationToken cancellationToken);
}
