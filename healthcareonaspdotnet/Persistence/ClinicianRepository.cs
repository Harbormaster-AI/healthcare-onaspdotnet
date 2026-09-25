
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class ClinicianRepository : IClinicianRepository
{
    private readonly ApplicationDbContext _db;

    public ClinicianRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Clinician?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Clinicians
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Clinician>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Clinicians
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Clinician clinician, CancellationToken cancellationToken)
    {
        _db.Clinicians.Add(clinician);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Clinician clinician, CancellationToken cancellationToken)
    {
        _db.Clinicians.Update(clinician);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Clinician clinician, CancellationToken cancellationToken)
    {
        _db.Clinicians.Remove(clinician);
        await _db.SaveChangesAsync(cancellationToken);
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


    public async Task AddToAppointmentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Appointments
            .Where(appointment =>
                request.ChildIds.Contains(appointment.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    appointment =>
                        EF.Property<Guid?>(
                            appointment,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromAppointmentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Appointments
            .Where(appointment =>
                request.ChildIds.Contains(appointment.Id) &&
                EF.Property<Guid?>(
                    appointment,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    appointment =>
                        EF.Property<Guid?>(
                            appointment,
                            "InventoryItem_Id"),
                    (Guid?)null));
    }


    public async Task AddToEncountersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Encounters
            .Where(encounter =>
                request.ChildIds.Contains(encounter.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    encounter =>
                        EF.Property<Guid?>(
                            encounter,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromEncountersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Encounters
            .Where(encounter =>
                request.ChildIds.Contains(encounter.Id) &&
                EF.Property<Guid?>(
                    encounter,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    encounter =>
                        EF.Property<Guid?>(
                            encounter,
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
