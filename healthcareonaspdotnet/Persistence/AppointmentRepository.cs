
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly ApplicationDbContext _db;

    public AppointmentRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Appointment?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Appointments
            .Include(x => x.Patient)
            .Include(x => x.Clinician)
            .Include(x => x.Facility)
            .Include(x => x.Encounter)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Appointment>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Appointments
            .AsNoTracking()
            .Include(x => x.Patient)
            .Include(x => x.Clinician)
            .Include(x => x.Facility)
            .Include(x => x.Encounter)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Appointment appointment, CancellationToken cancellationToken)
    {
        _db.Appointments.Add(appointment);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Appointment appointment, CancellationToken cancellationToken)
    {
        _db.Appointments.Update(appointment);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Appointment appointment, CancellationToken cancellationToken)
    {
        _db.Appointments.Remove(appointment);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
