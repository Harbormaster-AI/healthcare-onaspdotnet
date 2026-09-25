using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Persistence;

public interface IAppointmentRepository
{
    Task<Appointment?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Appointment>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Appointment appointment, CancellationToken cancellationToken);
    Task UpdateAsync(Appointment appointment, CancellationToken cancellationToken);
    Task DeleteAsync(Appointment appointment, CancellationToken cancellationToken);


}
