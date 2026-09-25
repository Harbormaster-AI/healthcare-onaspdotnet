using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Persistence;

public interface ICareTeamRepository
{
    Task<CareTeam?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<CareTeam>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(CareTeam careTeam, CancellationToken cancellationToken);
    Task UpdateAsync(CareTeam careTeam, CancellationToken cancellationToken);
    Task DeleteAsync(CareTeam careTeam, CancellationToken cancellationToken);

    Task AddToCliniciansAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCliniciansAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToPatientsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPatientsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
