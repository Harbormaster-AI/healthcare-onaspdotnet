
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class ClinicalOrderRepository : IClinicalOrderRepository
{
    private readonly ApplicationDbContext _db;

    public ClinicalOrderRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ClinicalOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ClinicalOrders
            .Include(x => x.Patient)
            .Include(x => x.Encounter)
            .Include(x => x.OrderingClinician)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ClinicalOrder>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ClinicalOrders
            .AsNoTracking()
            .Include(x => x.Patient)
            .Include(x => x.Encounter)
            .Include(x => x.OrderingClinician)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ClinicalOrder clinicalOrder, CancellationToken cancellationToken)
    {
        _db.ClinicalOrders.Add(clinicalOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ClinicalOrder clinicalOrder, CancellationToken cancellationToken)
    {
        _db.ClinicalOrders.Update(clinicalOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ClinicalOrder clinicalOrder, CancellationToken cancellationToken)
    {
        _db.ClinicalOrders.Remove(clinicalOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToMedicationOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.MedicationOrders
            .Where(medicationOrder =>
                request.ChildIds.Contains(medicationOrder.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    medicationOrder =>
                        EF.Property<Guid?>(
                            medicationOrder,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromMedicationOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.MedicationOrders
            .Where(medicationOrder =>
                request.ChildIds.Contains(medicationOrder.Id) &&
                EF.Property<Guid?>(
                    medicationOrder,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    medicationOrder =>
                        EF.Property<Guid?>(
                            medicationOrder,
                            "InventoryItem_Id"),
                    (Guid?)null));
    }


    public async Task AddToLaboratoryOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.LaboratoryOrders
            .Where(laboratoryOrder =>
                request.ChildIds.Contains(laboratoryOrder.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    laboratoryOrder =>
                        EF.Property<Guid?>(
                            laboratoryOrder,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromLaboratoryOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.LaboratoryOrders
            .Where(laboratoryOrder =>
                request.ChildIds.Contains(laboratoryOrder.Id) &&
                EF.Property<Guid?>(
                    laboratoryOrder,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    laboratoryOrder =>
                        EF.Property<Guid?>(
                            laboratoryOrder,
                            "InventoryItem_Id"),
                    (Guid?)null));
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


    public async Task AddToProcedureOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ProcedureOrders
            .Where(procedureOrder =>
                request.ChildIds.Contains(procedureOrder.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    procedureOrder =>
                        EF.Property<Guid?>(
                            procedureOrder,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromProcedureOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ProcedureOrders
            .Where(procedureOrder =>
                request.ChildIds.Contains(procedureOrder.Id) &&
                EF.Property<Guid?>(
                    procedureOrder,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    procedureOrder =>
                        EF.Property<Guid?>(
                            procedureOrder,
                            "InventoryItem_Id"),
                    (Guid?)null));
    }


    public async Task AddToAuthorizationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Authorizations
            .Where(authorization =>
                request.ChildIds.Contains(authorization.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    authorization =>
                        EF.Property<Guid?>(
                            authorization,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromAuthorizationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Authorizations
            .Where(authorization =>
                request.ChildIds.Contains(authorization.Id) &&
                EF.Property<Guid?>(
                    authorization,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    authorization =>
                        EF.Property<Guid?>(
                            authorization,
                            "InventoryItem_Id"),
                    (Guid?)null));
    }

}
