using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Persistence;

public interface ILabResultRepository
{
    Task<LabResult?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<LabResult>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(LabResult labResult, CancellationToken cancellationToken);
    Task UpdateAsync(LabResult labResult, CancellationToken cancellationToken);
    Task DeleteAsync(LabResult labResult, CancellationToken cancellationToken);

    Task AddToObservationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromObservationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
