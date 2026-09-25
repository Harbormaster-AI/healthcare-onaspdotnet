using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Persistence;

public interface IFacilityRepository
{
    Task<Facility?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Facility>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Facility facility, CancellationToken cancellationToken);
    Task UpdateAsync(Facility facility, CancellationToken cancellationToken);
    Task DeleteAsync(Facility facility, CancellationToken cancellationToken);

    Task AddToDepartmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDepartmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToCareTeamsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCareTeamsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToLaboratoriesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromLaboratoriesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToImagingCentersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromImagingCentersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToPharmaciesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPharmaciesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToInventoryItemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromInventoryItemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
