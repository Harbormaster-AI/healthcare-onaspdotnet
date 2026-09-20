using healthcareonaspdotnet.Domain;

namespace healthcareonaspdotnet.Persistence;

public interface IInsurancePayerRepository
{
    Task<InsurancePayer?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<InsurancePayer>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(InsurancePayer insurancePayer, CancellationToken cancellationToken);
    Task UpdateAsync(InsurancePayer insurancePayer, CancellationToken cancellationToken);
    Task DeleteAsync(InsurancePayer insurancePayer, CancellationToken cancellationToken);
}
