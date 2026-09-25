using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Persistence;

public interface IPatientRepository
{
    Task<Patient?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Patient>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Patient patient, CancellationToken cancellationToken);
    Task UpdateAsync(Patient patient, CancellationToken cancellationToken);
    Task DeleteAsync(Patient patient, CancellationToken cancellationToken);

    Task AddToAppointmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAppointmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToEncountersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromEncountersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToCarePlansAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCarePlansAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToAllergiesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAllergiesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToConditionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromConditionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToMedicationOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromMedicationOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToLabOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromLabOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToImagingOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromImagingOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToCoveragesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCoveragesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToClaimsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromClaimsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToDevicesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDevicesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToObservationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromObservationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
