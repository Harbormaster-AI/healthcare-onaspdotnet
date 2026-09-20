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
}
