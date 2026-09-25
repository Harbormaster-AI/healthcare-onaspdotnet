using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Persistence;

public interface IAllergyRepository
{
    Task<Allergy?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Allergy>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Allergy allergy, CancellationToken cancellationToken);
    Task UpdateAsync(Allergy allergy, CancellationToken cancellationToken);
    Task DeleteAsync(Allergy allergy, CancellationToken cancellationToken);


}
