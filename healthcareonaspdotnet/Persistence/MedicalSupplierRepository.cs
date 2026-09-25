
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class MedicalSupplierRepository : IMedicalSupplierRepository
{
    private readonly ApplicationDbContext _db;

    public MedicalSupplierRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<MedicalSupplier?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.MedicalSuppliers
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<MedicalSupplier>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.MedicalSuppliers
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(MedicalSupplier medicalSupplier, CancellationToken cancellationToken)
    {
        _db.MedicalSuppliers.Add(medicalSupplier);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(MedicalSupplier medicalSupplier, CancellationToken cancellationToken)
    {
        _db.MedicalSuppliers.Update(medicalSupplier);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(MedicalSupplier medicalSupplier, CancellationToken cancellationToken)
    {
        _db.MedicalSuppliers.Remove(medicalSupplier);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToFacilitiesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Facilitys
            .Where(facility =>
                request.ChildIds.Contains(facility.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    facility =>
                        EF.Property<Guid?>(
                            facility,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromFacilitiesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Facilitys
            .Where(facility =>
                request.ChildIds.Contains(facility.Id) &&
                EF.Property<Guid?>(
                    facility,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    facility =>
                        EF.Property<Guid?>(
                            facility,
                            "InventoryItem_Id"),
                    (Guid?)null));
    }


    public async Task AddToInventoryItemsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.InventoryItems
            .Where(inventoryItem =>
                request.ChildIds.Contains(inventoryItem.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    inventoryItem =>
                        EF.Property<Guid?>(
                            inventoryItem,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromInventoryItemsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.InventoryItems
            .Where(inventoryItem =>
                request.ChildIds.Contains(inventoryItem.Id) &&
                EF.Property<Guid?>(
                    inventoryItem,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    inventoryItem =>
                        EF.Property<Guid?>(
                            inventoryItem,
                            "InventoryItem_Id"),
                    (Guid?)null));
    }

}
