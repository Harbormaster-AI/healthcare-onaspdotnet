using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Persistence;

public interface IDischargeRepository
{
    Task<Discharge?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Discharge>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Discharge discharge, CancellationToken cancellationToken);
    Task UpdateAsync(Discharge discharge, CancellationToken cancellationToken);
    Task DeleteAsync(Discharge discharge, CancellationToken cancellationToken);


}
