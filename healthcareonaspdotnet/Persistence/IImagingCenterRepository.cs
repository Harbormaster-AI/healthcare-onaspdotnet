using healthcareonaspdotnet.Domain;

namespace healthcareonaspdotnet.Persistence;

public interface IImagingCenterRepository
{
    Task<ImagingCenter?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ImagingCenter>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ImagingCenter imagingCenter, CancellationToken cancellationToken);
    Task UpdateAsync(ImagingCenter imagingCenter, CancellationToken cancellationToken);
    Task DeleteAsync(ImagingCenter imagingCenter, CancellationToken cancellationToken);
}
