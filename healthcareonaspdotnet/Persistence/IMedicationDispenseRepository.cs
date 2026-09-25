using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Persistence;

public interface IMedicationDispenseRepository
{
    Task<MedicationDispense?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<MedicationDispense>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(MedicationDispense medicationDispense, CancellationToken cancellationToken);
    Task UpdateAsync(MedicationDispense medicationDispense, CancellationToken cancellationToken);
    Task DeleteAsync(MedicationDispense medicationDispense, CancellationToken cancellationToken);


}
