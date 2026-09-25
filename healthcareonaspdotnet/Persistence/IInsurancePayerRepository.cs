using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Persistence;

public interface IInsurancePayerRepository
{
    Task<InsurancePayer?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<InsurancePayer>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(InsurancePayer insurancePayer, CancellationToken cancellationToken);
    Task UpdateAsync(InsurancePayer insurancePayer, CancellationToken cancellationToken);
    Task DeleteAsync(InsurancePayer insurancePayer, CancellationToken cancellationToken);

    Task AddToPlansAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPlansAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToClaimsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromClaimsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
