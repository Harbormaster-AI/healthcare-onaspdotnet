using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Persistence;

public interface IClaimRepository
{
    Task<Claim?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Claim>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Claim claim, CancellationToken cancellationToken);
    Task UpdateAsync(Claim claim, CancellationToken cancellationToken);
    Task DeleteAsync(Claim claim, CancellationToken cancellationToken);

    Task AddToInvoicesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromInvoicesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
