
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class CareTeamRepository : ICareTeamRepository
{
    private readonly ApplicationDbContext _db;

    public CareTeamRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<CareTeam?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.CareTeams
            .Include(x => x.Department)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<CareTeam>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.CareTeams
            .AsNoTracking()
            .Include(x => x.Department)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(CareTeam careTeam, CancellationToken cancellationToken)
    {
        _db.CareTeams.Add(careTeam);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(CareTeam careTeam, CancellationToken cancellationToken)
    {
        _db.CareTeams.Update(careTeam);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(CareTeam careTeam, CancellationToken cancellationToken)
    {
        _db.CareTeams.Remove(careTeam);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToCliniciansAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Clinicians
            .Where(clinician =>
                request.ChildIds.Contains(clinician.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    clinician =>
                        EF.Property<Guid?>(
                            clinician,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromCliniciansAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Clinicians
            .Where(clinician =>
                request.ChildIds.Contains(clinician.Id) &&
                EF.Property<Guid?>(
                    clinician,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    clinician =>
                        EF.Property<Guid?>(
                            clinician,
                            "InventoryItem_Id"),
                    (Guid?)null));
    }


    public async Task AddToPatientsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Patients
            .Where(patient =>
                request.ChildIds.Contains(patient.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    patient =>
                        EF.Property<Guid?>(
                            patient,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromPatientsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Patients
            .Where(patient =>
                request.ChildIds.Contains(patient.Id) &&
                EF.Property<Guid?>(
                    patient,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    patient =>
                        EF.Property<Guid?>(
                            patient,
                            "InventoryItem_Id"),
                    (Guid?)null));
    }

}
