
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class LaboratoryOrderRepository : ILaboratoryOrderRepository
{
    private readonly ApplicationDbContext _db;

    public LaboratoryOrderRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<LaboratoryOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.LaboratoryOrders
            .Include(x => x.Order)
            .Include(x => x.Laboratory)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<LaboratoryOrder>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.LaboratoryOrders
            .AsNoTracking()
            .Include(x => x.Order)
            .Include(x => x.Laboratory)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(LaboratoryOrder laboratoryOrder, CancellationToken cancellationToken)
    {
        _db.LaboratoryOrders.Add(laboratoryOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(LaboratoryOrder laboratoryOrder, CancellationToken cancellationToken)
    {
        _db.LaboratoryOrders.Update(laboratoryOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(LaboratoryOrder laboratoryOrder, CancellationToken cancellationToken)
    {
        _db.LaboratoryOrders.Remove(laboratoryOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToResultsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.LabResults
            .Where(labResult =>
                request.ChildIds.Contains(labResult.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    labResult =>
                        EF.Property<Guid?>(
                            labResult,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromResultsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.LabResults
            .Where(labResult =>
                request.ChildIds.Contains(labResult.Id) &&
                EF.Property<Guid?>(
                    labResult,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    labResult =>
                        EF.Property<Guid?>(
                            labResult,
                            "InventoryItem_Id"),
                    (Guid?)null));
    }

}
