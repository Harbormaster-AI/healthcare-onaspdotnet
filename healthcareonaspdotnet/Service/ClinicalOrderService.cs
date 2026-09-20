using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Persistence;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Service;

public interface IClinicalOrderService {

    Task Create(ClinicalOrder model , CancellationToken cancellationToken);
    Task<bool> Update(ClinicalOrder model, CancellationToken cancellationToken);
    Task<ClinicalOrder?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<ClinicalOrder>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignPatient(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPatient(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignEncounter(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignEncounter(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignOrderingClinician(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignOrderingClinician(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToMedicationOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromMedicationOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToLaboratoryOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromLaboratoryOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToImagingOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromImagingOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToProcedureOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromProcedureOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToAuthorizations(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAuthorizations(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class ClinicalOrderService : IClinicalOrderService
{
    private readonly IClinicalOrderRepository _repository;
    private readonly ILogger<ClinicalOrderService> _logger;

    public ClinicalOrderService(
        IClinicalOrderRepository repository, ILogger<ClinicalOrderService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(ClinicalOrder model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(ClinicalOrder model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.OrderNumber = model.OrderNumber;
            existing.Status = model.Status;
            existing.OrderType = model.OrderType;
            existing.Priority = model.Priority;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<ClinicalOrder?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<ClinicalOrder>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignPatient(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignPatient(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignEncounter(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignEncounter(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignOrderingClinician(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignOrderingClinician(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToMedicationOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromMedicationOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToLaboratoryOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromLaboratoryOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToImagingOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromImagingOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToProcedureOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromProcedureOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToAuthorizations(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromAuthorizations(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
