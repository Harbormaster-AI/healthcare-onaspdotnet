
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly ApplicationDbContext _db;

    public DepartmentRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Department?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Departments
            .Include(x => x.Facility)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Department>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Departments
            .AsNoTracking()
            .Include(x => x.Facility)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Department department, CancellationToken cancellationToken)
    {
        _db.Departments.Add(department);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Department department, CancellationToken cancellationToken)
    {
        _db.Departments.Update(department);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Department department, CancellationToken cancellationToken)
    {
        _db.Departments.Remove(department);
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

}
