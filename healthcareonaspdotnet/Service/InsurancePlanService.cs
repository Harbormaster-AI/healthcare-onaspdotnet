
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Persistence;
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Telemetry;

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
    private readonly ApplicationTelemetry _telemetry;
    private readonly IInsurancePlanRepository _repository;
    private readonly ILogger<InsurancePlanService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public InsurancePlanService(
        ApplicationTelemetry telemetry,
        IInsurancePlanRepository repository,
        ILogger<InsurancePlanService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(InsurancePlan model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "InsurancePlan",
                "CreateInsurancePlan",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
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

            await _telemetry.Execute(
                "InsurancePlan",
                "UpdateInsurancePlan",
                () => _repository.UpdateAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
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
            await _telemetry.Execute(
                "InsurancePlan",
                "UpdateInsurancePlan",
                () => _repository.DeleteAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> AssignPayer(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No InsurancePlan found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<InsurancePayerService>().Get(childRequest, cancellationToken);
            parent.Payer = child;
            await Update( parent, cancellationToken );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> UnassignPayer(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No InsurancePlan found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Payer = null;
            await Update( parent, cancellationToken );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }


    public async Task<bool> AddToCoverages(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "InsurancePlan",
                "AddToCoverages",
                () => _repository.AddToCoveragesAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
           _logger.LogError(
                   ex,
                   "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> RemoveFromCoverages(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "InsurancePlan",
                "RemoveFromCoverages",
                () => _repository.RemoveFromCoveragesAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }



}
