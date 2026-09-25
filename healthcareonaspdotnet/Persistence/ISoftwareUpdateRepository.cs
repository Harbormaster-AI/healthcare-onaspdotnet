using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Persistence;

public interface ISoftwareUpdateRepository
{
    Task<SoftwareUpdate?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<SoftwareUpdate>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(SoftwareUpdate softwareUpdate, CancellationToken cancellationToken);
    Task UpdateAsync(SoftwareUpdate softwareUpdate, CancellationToken cancellationToken);
    Task DeleteAsync(SoftwareUpdate softwareUpdate, CancellationToken cancellationToken);


}
