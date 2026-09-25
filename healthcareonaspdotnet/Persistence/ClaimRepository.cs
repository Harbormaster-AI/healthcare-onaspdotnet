
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class ClaimRepository : IClaimRepository
{
    private readonly ApplicationDbContext _db;

    public ClaimRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Claim?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Claims
            .Include(x => x.Patient)
            .Include(x => x.Coverage)
            .Include(x => x.Encounter)
            .Include(x => x.Payer)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Claim>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Claims
            .AsNoTracking()
            .Include(x => x.Patient)
            .Include(x => x.Coverage)
            .Include(x => x.Encounter)
            .Include(x => x.Payer)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Claim claim, CancellationToken cancellationToken)
    {
        _db.Claims.Add(claim);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Claim claim, CancellationToken cancellationToken)
    {
        _db.Claims.Update(claim);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Claim claim, CancellationToken cancellationToken)
    {
        _db.Claims.Remove(claim);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToInvoicesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Invoices
            .Where(invoice =>
                request.ChildIds.Contains(invoice.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    invoice =>
                        EF.Property<Guid?>(
                            invoice,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromInvoicesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Invoices
            .Where(invoice =>
                request.ChildIds.Contains(invoice.Id) &&
                EF.Property<Guid?>(
                    invoice,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    invoice =>
                        EF.Property<Guid?>(
                            invoice,
                            "InventoryItem_Id"),
                    (Guid?)null));
    }

}
