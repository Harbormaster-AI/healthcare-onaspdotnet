using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Persistence;

public interface IClinicianRepository
{
    Task<Clinician?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Clinician>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Clinician clinician, CancellationToken cancellationToken);
    Task UpdateAsync(Clinician clinician, CancellationToken cancellationToken);
    Task DeleteAsync(Clinician clinician, CancellationToken cancellationToken);

    Task AddToCareTeamsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCareTeamsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToAppointmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAppointmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToEncountersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromEncountersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToProceduresAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromProceduresAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToImagingReportsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromImagingReportsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
