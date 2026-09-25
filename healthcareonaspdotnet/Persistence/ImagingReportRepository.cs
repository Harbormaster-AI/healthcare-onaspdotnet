
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class ImagingReportRepository : IImagingReportRepository
{
    private readonly ApplicationDbContext _db;

    public ImagingReportRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ImagingReport?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ImagingReports
            .Include(x => x.ImagingOrder)
            .Include(x => x.Clinician)
            .Include(x => x.Encounter)
            .Include(x => x.ImagingCenter)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ImagingReport>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ImagingReports
            .AsNoTracking()
            .Include(x => x.ImagingOrder)
            .Include(x => x.Clinician)
            .Include(x => x.Encounter)
            .Include(x => x.ImagingCenter)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ImagingReport imagingReport, CancellationToken cancellationToken)
    {
        _db.ImagingReports.Add(imagingReport);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ImagingReport imagingReport, CancellationToken cancellationToken)
    {
        _db.ImagingReports.Update(imagingReport);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ImagingReport imagingReport, CancellationToken cancellationToken)
    {
        _db.ImagingReports.Remove(imagingReport);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
