using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Persistence;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Service;

public interface IProcedureService {

    Task Create(Procedure model , CancellationToken cancellationToken);
    Task<bool> Update(Procedure model, CancellationToken cancellationToken);
    Task<Procedure?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Procedure>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignEncounter(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignEncounter(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignPerformer(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPerformer(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignProcedureOrder(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignProcedureOrder(AssociationRequest request, CancellationToken cancellationToken);


}

public class ProcedureService : IProcedureService
{
    private readonly IProcedureRepository _repository;
    private readonly ILogger<ProcedureService> _logger;

    public ProcedureService(
        IProcedureRepository repository, ILogger<ProcedureService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Procedure model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Procedure model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.ProcedureCode = model.ProcedureCode;
            existing.StartDateTime = model.StartDateTime;
            existing.EndDateTime = model.EndDateTime;
            existing.Status = model.Status;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<Procedure?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Procedure>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignEncounter(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignEncounter(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignPerformer(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignPerformer(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignProcedureOrder(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignProcedureOrder(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
