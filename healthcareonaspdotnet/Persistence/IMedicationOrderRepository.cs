using healthcareonaspdotnet.Domain;

namespace healthcareonaspdotnet.Persistence;

public interface IMedicationOrderRepository
{
    Task<MedicationOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<MedicationOrder>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(MedicationOrder medicationOrder, CancellationToken cancellationToken);
    Task UpdateAsync(MedicationOrder medicationOrder, CancellationToken cancellationToken);
    Task DeleteAsync(MedicationOrder medicationOrder, CancellationToken cancellationToken);
}
