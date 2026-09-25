
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class EncounterRepository : IEncounterRepository
{
    private readonly ApplicationDbContext _db;

    public EncounterRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Encounter?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Encounters
            .Include(x => x.Patient)
            .Include(x => x.Clinician)
            .Include(x => x.Facility)
            .Include(x => x.Appointment)
            .Include(x => x.Admission)
            .Include(x => x.Discharge)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Encounter>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Encounters
            .AsNoTracking()
            .Include(x => x.Patient)
            .Include(x => x.Clinician)
            .Include(x => x.Facility)
            .Include(x => x.Appointment)
            .Include(x => x.Admission)
            .Include(x => x.Discharge)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Encounter encounter, CancellationToken cancellationToken)
    {
        _db.Encounters.Add(encounter);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Encounter encounter, CancellationToken cancellationToken)
    {
        _db.Encounters.Update(encounter);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Encounter encounter, CancellationToken cancellationToken)
    {
        _db.Encounters.Remove(encounter);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToDiagnosesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Diagnosiss
            .Where(diagnosis =>
                request.ChildIds.Contains(diagnosis.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    diagnosis =>
                        EF.Property<Guid?>(
                            diagnosis,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromDiagnosesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Diagnosiss
            .Where(diagnosis =>
                request.ChildIds.Contains(diagnosis.Id) &&
                EF.Property<Guid?>(
                    diagnosis,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    diagnosis =>
                        EF.Property<Guid?>(
                            diagnosis,
                            "InventoryItem_Id"),
                    (Guid?)null));
    }


    public async Task AddToProceduresAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Procedures
            .Where(procedure =>
                request.ChildIds.Contains(procedure.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    procedure =>
                        EF.Property<Guid?>(
                            procedure,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromProceduresAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Procedures
            .Where(procedure =>
                request.ChildIds.Contains(procedure.Id) &&
                EF.Property<Guid?>(
                    procedure,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    procedure =>
                        EF.Property<Guid?>(
                            procedure,
                            "InventoryItem_Id"),
                    (Guid?)null));
    }


    public async Task AddToObservationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Observations
            .Where(observation =>
                request.ChildIds.Contains(observation.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    observation =>
                        EF.Property<Guid?>(
                            observation,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromObservationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Observations
            .Where(observation =>
                request.ChildIds.Contains(observation.Id) &&
                EF.Property<Guid?>(
                    observation,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    observation =>
                        EF.Property<Guid?>(
                            observation,
                            "InventoryItem_Id"),
                    (Guid?)null));
    }


    public async Task AddToOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ClinicalOrders
            .Where(clinicalOrder =>
                request.ChildIds.Contains(clinicalOrder.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    clinicalOrder =>
                        EF.Property<Guid?>(
                            clinicalOrder,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ClinicalOrders
            .Where(clinicalOrder =>
                request.ChildIds.Contains(clinicalOrder.Id) &&
                EF.Property<Guid?>(
                    clinicalOrder,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    clinicalOrder =>
                        EF.Property<Guid?>(
                            clinicalOrder,
                            "InventoryItem_Id"),
                    (Guid?)null));
    }

}
