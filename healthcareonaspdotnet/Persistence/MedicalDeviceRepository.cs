
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class MedicalDeviceRepository : IMedicalDeviceRepository
{
    private readonly ApplicationDbContext _db;

    public MedicalDeviceRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<MedicalDevice?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.MedicalDevices
            .Include(x => x.Patient)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<MedicalDevice>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.MedicalDevices
            .AsNoTracking()
            .Include(x => x.Patient)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(MedicalDevice medicalDevice, CancellationToken cancellationToken)
    {
        _db.MedicalDevices.Add(medicalDevice);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(MedicalDevice medicalDevice, CancellationToken cancellationToken)
    {
        _db.MedicalDevices.Update(medicalDevice);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(MedicalDevice medicalDevice, CancellationToken cancellationToken)
    {
        _db.MedicalDevices.Remove(medicalDevice);
        await _db.SaveChangesAsync(cancellationToken);
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


    public async Task AddToSoftwareUpdatesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.SoftwareUpdates
            .Where(softwareUpdate =>
                request.ChildIds.Contains(softwareUpdate.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    softwareUpdate =>
                        EF.Property<Guid?>(
                            softwareUpdate,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromSoftwareUpdatesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.SoftwareUpdates
            .Where(softwareUpdate =>
                request.ChildIds.Contains(softwareUpdate.Id) &&
                EF.Property<Guid?>(
                    softwareUpdate,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    softwareUpdate =>
                        EF.Property<Guid?>(
                            softwareUpdate,
                            "InventoryItem_Id"),
                    (Guid?)null));
    }

}
