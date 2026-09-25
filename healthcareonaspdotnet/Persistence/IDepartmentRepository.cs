using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Persistence;

public interface IDepartmentRepository
{
    Task<Department?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Department>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Department department, CancellationToken cancellationToken);
    Task UpdateAsync(Department department, CancellationToken cancellationToken);
    Task DeleteAsync(Department department, CancellationToken cancellationToken);

    Task AddToCareTeamsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCareTeamsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
