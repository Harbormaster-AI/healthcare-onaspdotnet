using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Persistence;

public interface IObservationRepository
{
    Task<Observation?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Observation>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Observation observation, CancellationToken cancellationToken);
    Task UpdateAsync(Observation observation, CancellationToken cancellationToken);
    Task DeleteAsync(Observation observation, CancellationToken cancellationToken);


}
