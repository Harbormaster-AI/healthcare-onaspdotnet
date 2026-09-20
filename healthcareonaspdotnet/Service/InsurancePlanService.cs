using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Persistence;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Service;

public interface IInsurancePlanService {

    Task Create(InsurancePlan model , CancellationToken cancellationToken);
    Task<bool> Update(InsurancePlan model, CancellationToken cancellationToken);
    Task<InsurancePlan?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<InsurancePlan>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignPayer(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPayer(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToCoverages(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCoverages(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class InsurancePlanService : IInsurancePlanService
{
    private readonly IInsurancePlanRepository _repository;
    private readonly ILogger<InsurancePlanService> _logger;

    public InsurancePlanService(
        IInsurancePlanRepository repository, ILogger<InsurancePlanService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(InsurancePlan model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(InsurancePlan model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.PlanCode = model.PlanCode;
            existing.PlanType = model.PlanType;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<InsurancePlan?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<InsurancePlan>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignPayer(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignPayer(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToCoverages(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromCoverages(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
