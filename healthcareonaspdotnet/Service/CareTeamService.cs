using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Persistence;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Service;

public interface ICareTeamService {

    Task Create(CareTeam model , CancellationToken cancellationToken);
    Task<bool> Update(CareTeam model, CancellationToken cancellationToken);
    Task<CareTeam?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<CareTeam>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignDepartment(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignDepartment(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToClinicians(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromClinicians(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToPatients(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPatients(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class CareTeamService : ICareTeamService
{
    private readonly ICareTeamRepository _repository;
    private readonly ILogger<CareTeamService> _logger;

    public CareTeamService(
        ICareTeamRepository repository, ILogger<CareTeamService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(CareTeam model, CancellationToken cancellationToken)
    {

         try
        {
            await _repository.AddAsync(model, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
        }
    }

    public async Task<bool> Update(CareTeam model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.CareSetting = model.CareSetting;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<CareTeam?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<CareTeam>> GetAll(CancellationToken cancellationToken)
    => _repository.GetAllAsync(cancellationToken);

    public async Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(identifier.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        try
        {
            await _repository.DeleteAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;

    }

    public async Task<bool> AssignDepartment(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignDepartment(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToClinicians(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromClinicians(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToPatients(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromPatients(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
