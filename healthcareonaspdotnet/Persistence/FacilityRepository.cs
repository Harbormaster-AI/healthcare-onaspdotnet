
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class FacilityRepository : IFacilityRepository
{
    private readonly ApplicationDbContext _db;

    public FacilityRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Facility?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Facilitys
            .Include(x => x.HealthSystem)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Facility>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Facilitys
            .AsNoTracking()
            .Include(x => x.HealthSystem)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Facility facility, CancellationToken cancellationToken)
    {
        _db.Facilitys.Add(facility);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Facility facility, CancellationToken cancellationToken)
    {
        _db.Facilitys.Update(facility);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Facility facility, CancellationToken cancellationToken)
    {
        _db.Facilitys.Remove(facility);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToDepartmentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Departments
            .Where(department =>
                request.ChildIds.Contains(department.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    department =>
                        EF.Property<Guid?>(
                            department,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromDepartmentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Departments
            .Where(department =>
                request.ChildIds.Contains(department.Id) &&
                EF.Property<Guid?>(
                    department,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    department =>
                        EF.Property<Guid?>(
                            department,
                            "InventoryItem_Id"),
                    (Guid?)null));
    }


    public async Task AddToCareTeamsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CareTeams
            .Where(careTeam =>
                request.ChildIds.Contains(careTeam.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    careTeam =>
                        EF.Property<Guid?>(
                            careTeam,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromCareTeamsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CareTeams
            .Where(careTeam =>
                request.ChildIds.Contains(careTeam.Id) &&
                EF.Property<Guid?>(
                    careTeam,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    careTeam =>
                        EF.Property<Guid?>(
                            careTeam,
                            "InventoryItem_Id"),
                    (Guid?)null));
    }


    public async Task AddToLaboratoriesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Laboratorys
            .Where(laboratory =>
                request.ChildIds.Contains(laboratory.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    laboratory =>
                        EF.Property<Guid?>(
                            laboratory,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromLaboratoriesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Laboratorys
            .Where(laboratory =>
                request.ChildIds.Contains(laboratory.Id) &&
                EF.Property<Guid?>(
                    laboratory,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    laboratory =>
                        EF.Property<Guid?>(
                            laboratory,
                            "InventoryItem_Id"),
                    (Guid?)null));
    }


    public async Task AddToImagingCentersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ImagingCenters
            .Where(imagingCenter =>
                request.ChildIds.Contains(imagingCenter.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    imagingCenter =>
                        EF.Property<Guid?>(
                            imagingCenter,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromImagingCentersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ImagingCenters
            .Where(imagingCenter =>
                request.ChildIds.Contains(imagingCenter.Id) &&
                EF.Property<Guid?>(
                    imagingCenter,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    imagingCenter =>
                        EF.Property<Guid?>(
                            imagingCenter,
                            "InventoryItem_Id"),
                    (Guid?)null));
    }


    public async Task AddToPharmaciesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Pharmacys
            .Where(pharmacy =>
                request.ChildIds.Contains(pharmacy.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    pharmacy =>
                        EF.Property<Guid?>(
                            pharmacy,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromPharmaciesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Pharmacys
            .Where(pharmacy =>
                request.ChildIds.Contains(pharmacy.Id) &&
                EF.Property<Guid?>(
                    pharmacy,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    pharmacy =>
                        EF.Property<Guid?>(
                            pharmacy,
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
