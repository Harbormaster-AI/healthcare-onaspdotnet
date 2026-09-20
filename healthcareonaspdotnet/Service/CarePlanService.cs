using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Persistence;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Service;

public interface ICarePlanService {

    Task Create(CarePlan model , CancellationToken cancellationToken);
    Task<bool> Update(CarePlan model, CancellationToken cancellationToken);
    Task<CarePlan?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<CarePlan>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignPatient(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPatient(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignCareTeam(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCareTeam(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToEncounters(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromEncounters(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToTasks(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromTasks(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class CarePlanService : ICarePlanService
{
    private readonly ICarePlanRepository _repository;
    private readonly ILogger<CarePlanService> _logger;

    public CarePlanService(
        ICarePlanRepository repository, ILogger<CarePlanService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(CarePlan model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(CarePlan model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.PlanNumber = model.PlanNumber;
            existing.GoalSummary = model.GoalSummary;
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

    public Task<CarePlan?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<CarePlan>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignCareTeam(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignCareTeam(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToEncounters(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromEncounters(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToTasks(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromTasks(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
