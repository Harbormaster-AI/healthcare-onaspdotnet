using healthcareonaspdotnet.Domain;

namespace healthcareonaspdotnet.Persistence;

public interface ICarePlanRepository
{
    Task<CarePlan?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<CarePlan>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(CarePlan carePlan, CancellationToken cancellationToken);
    Task UpdateAsync(CarePlan carePlan, CancellationToken cancellationToken);
    Task DeleteAsync(CarePlan carePlan, CancellationToken cancellationToken);
}
