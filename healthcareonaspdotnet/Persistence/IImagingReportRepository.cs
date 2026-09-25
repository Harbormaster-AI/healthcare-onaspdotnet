using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Persistence;

public interface IImagingReportRepository
{
    Task<ImagingReport?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ImagingReport>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ImagingReport imagingReport, CancellationToken cancellationToken);
    Task UpdateAsync(ImagingReport imagingReport, CancellationToken cancellationToken);
    Task DeleteAsync(ImagingReport imagingReport, CancellationToken cancellationToken);


}
