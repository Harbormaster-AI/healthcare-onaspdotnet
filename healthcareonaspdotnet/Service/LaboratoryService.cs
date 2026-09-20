using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Persistence;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Service;

public interface ILaboratoryService {

    Task Create(Laboratory model , CancellationToken cancellationToken);
    Task<bool> Update(Laboratory model, CancellationToken cancellationToken);
    Task<Laboratory?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Laboratory>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignFacility(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignFacility(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToLaboratoryOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromLaboratoryOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToLabResults(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromLabResults(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class LaboratoryService : ILaboratoryService
{
    private readonly ILaboratoryRepository _repository;
    private readonly ILogger<LaboratoryService> _logger;

    public LaboratoryService(
        ILaboratoryRepository repository, ILogger<LaboratoryService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Laboratory model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Laboratory model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.CliaNumber = model.CliaNumber;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<Laboratory?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Laboratory>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignFacility(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignFacility(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToLaboratoryOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromLaboratoryOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToLabResults(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromLabResults(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
