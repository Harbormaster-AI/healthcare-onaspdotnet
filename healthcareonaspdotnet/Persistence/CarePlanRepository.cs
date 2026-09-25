
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class CarePlanRepository : ICarePlanRepository
{
    private readonly ApplicationDbContext _db;

    public CarePlanRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<CarePlan?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.CarePlans
            .Include(x => x.Patient)
            .Include(x => x.CareTeam)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<CarePlan>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.CarePlans
            .AsNoTracking()
            .Include(x => x.Patient)
            .Include(x => x.CareTeam)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(CarePlan carePlan, CancellationToken cancellationToken)
    {
        _db.CarePlans.Add(carePlan);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(CarePlan carePlan, CancellationToken cancellationToken)
    {
        _db.CarePlans.Update(carePlan);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(CarePlan carePlan, CancellationToken cancellationToken)
    {
        _db.CarePlans.Remove(carePlan);
        await _db.SaveChangesAsync(cancellationToken);
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


    public async Task AddToTasksAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CareTasks
            .Where(careTask =>
                request.ChildIds.Contains(careTask.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    careTask =>
                        EF.Property<Guid?>(
                            careTask,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromTasksAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CareTasks
            .Where(careTask =>
                request.ChildIds.Contains(careTask.Id) &&
                EF.Property<Guid?>(
                    careTask,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    careTask =>
                        EF.Property<Guid?>(
                            careTask,
                            "InventoryItem_Id"),
                    (Guid?)null));
    }

}
