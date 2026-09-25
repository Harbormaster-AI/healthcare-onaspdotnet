using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Persistence;

public interface IClinicalOrderRepository
{
    Task<ClinicalOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ClinicalOrder>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ClinicalOrder clinicalOrder, CancellationToken cancellationToken);
    Task UpdateAsync(ClinicalOrder clinicalOrder, CancellationToken cancellationToken);
    Task DeleteAsync(ClinicalOrder clinicalOrder, CancellationToken cancellationToken);

    Task AddToMedicationOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromMedicationOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToLaboratoryOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromLaboratoryOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToImagingOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromImagingOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToProcedureOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromProcedureOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToAuthorizationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAuthorizationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
