using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Persistence;

public interface IHealthSystemRepository
{
    Task<HealthSystem?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<HealthSystem>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(HealthSystem healthSystem, CancellationToken cancellationToken);
    Task UpdateAsync(HealthSystem healthSystem, CancellationToken cancellationToken);
    Task DeleteAsync(HealthSystem healthSystem, CancellationToken cancellationToken);

    Task AddToFacilitiesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromFacilitiesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToSuppliersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromSuppliersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
