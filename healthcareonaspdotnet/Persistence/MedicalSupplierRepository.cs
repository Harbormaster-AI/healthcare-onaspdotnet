using healthcareonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class MedicalSupplierRepository : IMedicalSupplierRepository
{
    private readonly ApplicationDbContext _db;

    public MedicalSupplierRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<MedicalSupplier?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.MedicalSuppliers
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<MedicalSupplier>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.MedicalSuppliers
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(MedicalSupplier medicalSupplier, CancellationToken cancellationToken)
    {
        _db.MedicalSuppliers.Add(medicalSupplier);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(MedicalSupplier medicalSupplier, CancellationToken cancellationToken)
    {
        _db.MedicalSuppliers.Update(medicalSupplier);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(MedicalSupplier medicalSupplier, CancellationToken cancellationToken)
    {
        _db.MedicalSuppliers.Remove(medicalSupplier);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
