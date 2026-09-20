using healthcareonaspdotnet.Domain;

namespace healthcareonaspdotnet.Persistence;

public interface IInsurancePlanRepository
{
    Task<InsurancePlan?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<InsurancePlan>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(InsurancePlan insurancePlan, CancellationToken cancellationToken);
    Task UpdateAsync(InsurancePlan insurancePlan, CancellationToken cancellationToken);
    Task DeleteAsync(InsurancePlan insurancePlan, CancellationToken cancellationToken);
}
