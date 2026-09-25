using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Persistence;

public interface ICarePlanRepository
{
    Task<CarePlan?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<CarePlan>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(CarePlan carePlan, CancellationToken cancellationToken);
    Task UpdateAsync(CarePlan carePlan, CancellationToken cancellationToken);
    Task DeleteAsync(CarePlan carePlan, CancellationToken cancellationToken);

    Task AddToEncountersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromEncountersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToTasksAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromTasksAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
