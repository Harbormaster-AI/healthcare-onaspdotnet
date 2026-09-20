using healthcareonaspdotnet.Domain;

namespace healthcareonaspdotnet.Persistence;

public interface IFacilityRepository
{
    Task<Facility?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Facility>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Facility facility, CancellationToken cancellationToken);
    Task UpdateAsync(Facility facility, CancellationToken cancellationToken);
    Task DeleteAsync(Facility facility, CancellationToken cancellationToken);
}
