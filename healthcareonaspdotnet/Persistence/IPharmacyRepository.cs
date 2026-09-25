using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Persistence;

public interface IPharmacyRepository
{
    Task<Pharmacy?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Pharmacy>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Pharmacy pharmacy, CancellationToken cancellationToken);
    Task UpdateAsync(Pharmacy pharmacy, CancellationToken cancellationToken);
    Task DeleteAsync(Pharmacy pharmacy, CancellationToken cancellationToken);

    Task AddToMedicationDispensesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromMedicationDispensesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToMedicationOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromMedicationOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
