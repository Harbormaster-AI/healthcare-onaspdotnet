using healthcareonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class InsurancePayerRepository : IInsurancePayerRepository
{
    private readonly ApplicationDbContext _db;

    public InsurancePayerRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<InsurancePayer?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.InsurancePayers
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<InsurancePayer>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.InsurancePayers
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(InsurancePayer insurancePayer, CancellationToken cancellationToken)
    {
        _db.InsurancePayers.Add(insurancePayer);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(InsurancePayer insurancePayer, CancellationToken cancellationToken)
    {
        _db.InsurancePayers.Update(insurancePayer);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(InsurancePayer insurancePayer, CancellationToken cancellationToken)
    {
        _db.InsurancePayers.Remove(insurancePayer);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
