using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Persistence;

public interface IImagingOrderRepository
{
    Task<ImagingOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ImagingOrder>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ImagingOrder imagingOrder, CancellationToken cancellationToken);
    Task UpdateAsync(ImagingOrder imagingOrder, CancellationToken cancellationToken);
    Task DeleteAsync(ImagingOrder imagingOrder, CancellationToken cancellationToken);

    Task AddToReportsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromReportsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
