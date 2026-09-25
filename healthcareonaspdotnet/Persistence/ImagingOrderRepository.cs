
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class ImagingOrderRepository : IImagingOrderRepository
{
    private readonly ApplicationDbContext _db;

    public ImagingOrderRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ImagingOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ImagingOrders
            .Include(x => x.Order)
            .Include(x => x.ImagingCenter)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ImagingOrder>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ImagingOrders
            .AsNoTracking()
            .Include(x => x.Order)
            .Include(x => x.ImagingCenter)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ImagingOrder imagingOrder, CancellationToken cancellationToken)
    {
        _db.ImagingOrders.Add(imagingOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ImagingOrder imagingOrder, CancellationToken cancellationToken)
    {
        _db.ImagingOrders.Update(imagingOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ImagingOrder imagingOrder, CancellationToken cancellationToken)
    {
        _db.ImagingOrders.Remove(imagingOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToReportsAsync(
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

    public async Task RemoveFromReportsAsync(
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
