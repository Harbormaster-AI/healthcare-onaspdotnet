
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class HealthSystemRepository : IHealthSystemRepository
{
    private readonly ApplicationDbContext _db;

    public HealthSystemRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<HealthSystem?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.HealthSystems
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<HealthSystem>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.HealthSystems
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(HealthSystem healthSystem, CancellationToken cancellationToken)
    {
        _db.HealthSystems.Add(healthSystem);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(HealthSystem healthSystem, CancellationToken cancellationToken)
    {
        _db.HealthSystems.Update(healthSystem);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(HealthSystem healthSystem, CancellationToken cancellationToken)
    {
        _db.HealthSystems.Remove(healthSystem);
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


    public async Task AddToSuppliersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.MedicalSuppliers
            .Where(medicalSupplier =>
                request.ChildIds.Contains(medicalSupplier.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    medicalSupplier =>
                        EF.Property<Guid?>(
                            medicalSupplier,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromSuppliersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.MedicalSuppliers
            .Where(medicalSupplier =>
                request.ChildIds.Contains(medicalSupplier.Id) &&
                EF.Property<Guid?>(
                    medicalSupplier,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    medicalSupplier =>
                        EF.Property<Guid?>(
                            medicalSupplier,
                            "InventoryItem_Id"),
                    (Guid?)null));
    }

}
