using healthcareonaspdotnet.Domain;

namespace healthcareonaspdotnet.Persistence;

public interface IPharmacyRepository
{
    Task<Pharmacy?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Pharmacy>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Pharmacy pharmacy, CancellationToken cancellationToken);
    Task UpdateAsync(Pharmacy pharmacy, CancellationToken cancellationToken);
    Task DeleteAsync(Pharmacy pharmacy, CancellationToken cancellationToken);
}
