
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Persistence;
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Telemetry;

namespace healthcareonaspdotnet.Service;

public interface IInsurancePayerService {

    Task Create(InsurancePayer model , CancellationToken cancellationToken);
    Task<bool> Update(InsurancePayer model, CancellationToken cancellationToken);
    Task<InsurancePayer?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<InsurancePayer>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToPlans(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPlans(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToClaims(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromClaims(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class InsurancePayerService : IInsurancePayerService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IInsurancePayerRepository _repository;
    private readonly ILogger<InsurancePayerService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public InsurancePayerService(
        ApplicationTelemetry telemetry,
        IInsurancePayerRepository repository,
        ILogger<InsurancePayerService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(InsurancePayer model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "InsurancePayer",
                "CreateInsurancePayer",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(InsurancePayer model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.Website = model.Website;
            existing.PayerType = model.PayerType;

            await _telemetry.Execute(
                "InsurancePayer",
                "UpdateInsurancePayer",
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

    public Task<InsurancePayer?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<InsurancePayer>> GetAll(CancellationToken cancellationToken)
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
                "InsurancePayer",
                "UpdateInsurancePayer",
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


    public async Task<bool> AddToPlans(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "InsurancePayer",
                "AddToPlans",
                () => _repository.AddToPlansAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromPlans(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "InsurancePayer",
                "RemoveFromPlans",
                () => _repository.RemoveFromPlansAsync(request, cancellationToken));
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

    public async Task<bool> AddToClaims(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "InsurancePayer",
                "AddToClaims",
                () => _repository.AddToClaimsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromClaims(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "InsurancePayer",
                "RemoveFromClaims",
                () => _repository.RemoveFromClaimsAsync(request, cancellationToken));
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
