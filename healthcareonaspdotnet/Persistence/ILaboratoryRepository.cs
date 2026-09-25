using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Persistence;

public interface ILaboratoryRepository
{
    Task<Laboratory?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Laboratory>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Laboratory laboratory, CancellationToken cancellationToken);
    Task UpdateAsync(Laboratory laboratory, CancellationToken cancellationToken);
    Task DeleteAsync(Laboratory laboratory, CancellationToken cancellationToken);

    Task AddToLaboratoryOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromLaboratoryOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToLabResultsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromLabResultsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
