
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class MedicationOrderRepository : IMedicationOrderRepository
{
    private readonly ApplicationDbContext _db;

    public MedicationOrderRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<MedicationOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.MedicationOrders
            .Include(x => x.Order)
            .Include(x => x.Pharmacy)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<MedicationOrder>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.MedicationOrders
            .AsNoTracking()
            .Include(x => x.Order)
            .Include(x => x.Pharmacy)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(MedicationOrder medicationOrder, CancellationToken cancellationToken)
    {
        _db.MedicationOrders.Add(medicationOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(MedicationOrder medicationOrder, CancellationToken cancellationToken)
    {
        _db.MedicationOrders.Update(medicationOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(MedicationOrder medicationOrder, CancellationToken cancellationToken)
    {
        _db.MedicationOrders.Remove(medicationOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToDispensesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.MedicationDispenses
            .Where(medicationDispense =>
                request.ChildIds.Contains(medicationDispense.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    medicationDispense =>
                        EF.Property<Guid?>(
                            medicationDispense,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromDispensesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.MedicationDispenses
            .Where(medicationDispense =>
                request.ChildIds.Contains(medicationDispense.Id) &&
                EF.Property<Guid?>(
                    medicationDispense,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    medicationDispense =>
                        EF.Property<Guid?>(
                            medicationDispense,
                            "InventoryItem_Id"),
                    (Guid?)null));
    }

}
