using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Persistence;

public interface IEncounterRepository
{
    Task<Encounter?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Encounter>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Encounter encounter, CancellationToken cancellationToken);
    Task UpdateAsync(Encounter encounter, CancellationToken cancellationToken);
    Task DeleteAsync(Encounter encounter, CancellationToken cancellationToken);

    Task AddToDiagnosesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDiagnosesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToProceduresAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromProceduresAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToObservationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromObservationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
