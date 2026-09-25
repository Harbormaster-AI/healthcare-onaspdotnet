using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Persistence;

public interface IProcedureOrderRepository
{
    Task<ProcedureOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ProcedureOrder>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ProcedureOrder procedureOrder, CancellationToken cancellationToken);
    Task UpdateAsync(ProcedureOrder procedureOrder, CancellationToken cancellationToken);
    Task DeleteAsync(ProcedureOrder procedureOrder, CancellationToken cancellationToken);


}
