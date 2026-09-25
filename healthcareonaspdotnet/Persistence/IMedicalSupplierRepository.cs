using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Persistence;

public interface IMedicalSupplierRepository
{
    Task<MedicalSupplier?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<MedicalSupplier>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(MedicalSupplier medicalSupplier, CancellationToken cancellationToken);
    Task UpdateAsync(MedicalSupplier medicalSupplier, CancellationToken cancellationToken);
    Task DeleteAsync(MedicalSupplier medicalSupplier, CancellationToken cancellationToken);

    Task AddToFacilitiesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromFacilitiesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToInventoryItemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromInventoryItemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
