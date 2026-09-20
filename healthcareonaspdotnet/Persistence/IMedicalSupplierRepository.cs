using healthcareonaspdotnet.Domain;

namespace healthcareonaspdotnet.Persistence;

public interface IMedicalSupplierRepository
{
    Task<MedicalSupplier?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<MedicalSupplier>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(MedicalSupplier medicalSupplier, CancellationToken cancellationToken);
    Task UpdateAsync(MedicalSupplier medicalSupplier, CancellationToken cancellationToken);
    Task DeleteAsync(MedicalSupplier medicalSupplier, CancellationToken cancellationToken);
}
