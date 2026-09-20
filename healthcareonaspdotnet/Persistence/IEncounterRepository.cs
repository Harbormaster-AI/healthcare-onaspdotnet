using healthcareonaspdotnet.Domain;

namespace healthcareonaspdotnet.Persistence;

public interface IEncounterRepository
{
    Task<Encounter?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Encounter>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Encounter encounter, CancellationToken cancellationToken);
    Task UpdateAsync(Encounter encounter, CancellationToken cancellationToken);
    Task DeleteAsync(Encounter encounter, CancellationToken cancellationToken);
}
