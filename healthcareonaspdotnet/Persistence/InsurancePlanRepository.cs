
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class InsurancePlanRepository : IInsurancePlanRepository
{
    private readonly ApplicationDbContext _db;

    public InsurancePlanRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<InsurancePlan?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.InsurancePlans
            .Include(x => x.Payer)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<InsurancePlan>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.InsurancePlans
            .AsNoTracking()
            .Include(x => x.Payer)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(InsurancePlan insurancePlan, CancellationToken cancellationToken)
    {
        _db.InsurancePlans.Add(insurancePlan);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(InsurancePlan insurancePlan, CancellationToken cancellationToken)
    {
        _db.InsurancePlans.Update(insurancePlan);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(InsurancePlan insurancePlan, CancellationToken cancellationToken)
    {
        _db.InsurancePlans.Remove(insurancePlan);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToCoveragesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Coverages
            .Where(coverage =>
                request.ChildIds.Contains(coverage.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    coverage =>
                        EF.Property<Guid?>(
                            coverage,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromCoveragesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Coverages
            .Where(coverage =>
                request.ChildIds.Contains(coverage.Id) &&
                EF.Property<Guid?>(
                    coverage,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    coverage =>
                        EF.Property<Guid?>(
                            coverage,
                            "InventoryItem_Id"),
                    (Guid?)null));
    }

}
