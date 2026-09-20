using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Persistence;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Service;

public interface ILaboratoryOrderService {

    Task Create(LaboratoryOrder model , CancellationToken cancellationToken);
    Task<bool> Update(LaboratoryOrder model, CancellationToken cancellationToken);
    Task<LaboratoryOrder?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<LaboratoryOrder>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignOrder(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignOrder(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignLaboratory(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignLaboratory(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToResults(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromResults(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class LaboratoryOrderService : ILaboratoryOrderService
{
    private readonly ILaboratoryOrderRepository _repository;
    private readonly ILogger<LaboratoryOrderService> _logger;

    public LaboratoryOrderService(
        ILaboratoryOrderRepository repository, ILogger<LaboratoryOrderService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(LaboratoryOrder model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(LaboratoryOrder model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.TestCode = model.TestCode;
            existing.FastingRequired = model.FastingRequired;
            existing.SpecimenType = model.SpecimenType;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<LaboratoryOrder?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<LaboratoryOrder>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignOrder(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignOrder(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignLaboratory(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignLaboratory(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToResults(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromResults(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
