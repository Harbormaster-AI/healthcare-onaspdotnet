
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class MedicationDispenseRepository : IMedicationDispenseRepository
{
    private readonly ApplicationDbContext _db;

    public MedicationDispenseRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<MedicationDispense?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.MedicationDispenses
            .Include(x => x.MedicationOrder)
            .Include(x => x.Pharmacy)
            .Include(x => x.Patient)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<MedicationDispense>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.MedicationDispenses
            .AsNoTracking()
            .Include(x => x.MedicationOrder)
            .Include(x => x.Pharmacy)
            .Include(x => x.Patient)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(MedicationDispense medicationDispense, CancellationToken cancellationToken)
    {
        _db.MedicationDispenses.Add(medicationDispense);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(MedicationDispense medicationDispense, CancellationToken cancellationToken)
    {
        _db.MedicationDispenses.Update(medicationDispense);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(MedicationDispense medicationDispense, CancellationToken cancellationToken)
    {
        _db.MedicationDispenses.Remove(medicationDispense);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
