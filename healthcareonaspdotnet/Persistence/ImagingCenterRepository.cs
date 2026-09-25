
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class ImagingCenterRepository : IImagingCenterRepository
{
    private readonly ApplicationDbContext _db;

    public ImagingCenterRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ImagingCenter?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ImagingCenters
            .Include(x => x.Facility)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ImagingCenter>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ImagingCenters
            .AsNoTracking()
            .Include(x => x.Facility)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ImagingCenter imagingCenter, CancellationToken cancellationToken)
    {
        _db.ImagingCenters.Add(imagingCenter);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ImagingCenter imagingCenter, CancellationToken cancellationToken)
    {
        _db.ImagingCenters.Update(imagingCenter);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ImagingCenter imagingCenter, CancellationToken cancellationToken)
    {
        _db.ImagingCenters.Remove(imagingCenter);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToImagingOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ImagingOrders
            .Where(imagingOrder =>
                request.ChildIds.Contains(imagingOrder.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    imagingOrder =>
                        EF.Property<Guid?>(
                            imagingOrder,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromImagingOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ImagingOrders
            .Where(imagingOrder =>
                request.ChildIds.Contains(imagingOrder.Id) &&
                EF.Property<Guid?>(
                    imagingOrder,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    imagingOrder =>
                        EF.Property<Guid?>(
                            imagingOrder,
                            "InventoryItem_Id"),
                    (Guid?)null));
    }


    public async Task AddToImagingReportsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ImagingReports
            .Where(imagingReport =>
                request.ChildIds.Contains(imagingReport.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    imagingReport =>
                        EF.Property<Guid?>(
                            imagingReport,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromImagingReportsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ImagingReports
            .Where(imagingReport =>
                request.ChildIds.Contains(imagingReport.Id) &&
                EF.Property<Guid?>(
                    imagingReport,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    imagingReport =>
                        EF.Property<Guid?>(
                            imagingReport,
                            "InventoryItem_Id"),
                    (Guid?)null));
    }

}
