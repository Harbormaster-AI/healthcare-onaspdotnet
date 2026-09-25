using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Persistence;

public interface IAdmissionRepository
{
    Task<Admission?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Admission>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Admission admission, CancellationToken cancellationToken);
    Task UpdateAsync(Admission admission, CancellationToken cancellationToken);
    Task DeleteAsync(Admission admission, CancellationToken cancellationToken);


}
