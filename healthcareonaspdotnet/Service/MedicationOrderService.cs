using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Persistence;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Service;

public interface IMedicationOrderService {

    Task Create(MedicationOrder model , CancellationToken cancellationToken);
    Task<bool> Update(MedicationOrder model, CancellationToken cancellationToken);
    Task<MedicationOrder?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<MedicationOrder>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignOrder(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignOrder(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignPharmacy(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPharmacy(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToDispenses(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDispenses(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class MedicationOrderService : IMedicationOrderService
{
    private readonly IMedicationOrderRepository _repository;
    private readonly ILogger<MedicationOrderService> _logger;

    public MedicationOrderService(
        IMedicationOrderRepository repository, ILogger<MedicationOrderService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(MedicationOrder model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(MedicationOrder model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.MedicationCode = model.MedicationCode;
            existing.Dose = model.Dose;
            existing.Frequency = model.Frequency;
            existing.Duration = model.Duration;
            existing.Route = model.Route;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<MedicationOrder?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<MedicationOrder>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignPharmacy(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignPharmacy(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToDispenses(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromDispenses(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
