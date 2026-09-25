using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Persistence;

public interface ICoverageRepository
{
    Task<Coverage?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Coverage>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Coverage coverage, CancellationToken cancellationToken);
    Task UpdateAsync(Coverage coverage, CancellationToken cancellationToken);
    Task DeleteAsync(Coverage coverage, CancellationToken cancellationToken);

    Task AddToClaimsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromClaimsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToAuthorizationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAuthorizationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
