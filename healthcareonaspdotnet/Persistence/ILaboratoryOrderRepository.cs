using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Persistence;

public interface ILaboratoryOrderRepository
{
    Task<LaboratoryOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<LaboratoryOrder>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(LaboratoryOrder laboratoryOrder, CancellationToken cancellationToken);
    Task UpdateAsync(LaboratoryOrder laboratoryOrder, CancellationToken cancellationToken);
    Task DeleteAsync(LaboratoryOrder laboratoryOrder, CancellationToken cancellationToken);

    Task AddToResultsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromResultsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
