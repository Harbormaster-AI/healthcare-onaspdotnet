using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Persistence;

public interface IImagingCenterRepository
{
    Task<ImagingCenter?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ImagingCenter>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ImagingCenter imagingCenter, CancellationToken cancellationToken);
    Task UpdateAsync(ImagingCenter imagingCenter, CancellationToken cancellationToken);
    Task DeleteAsync(ImagingCenter imagingCenter, CancellationToken cancellationToken);

    Task AddToImagingOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromImagingOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToImagingReportsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromImagingReportsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
