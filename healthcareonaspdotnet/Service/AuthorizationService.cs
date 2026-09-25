
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Persistence;
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Telemetry;

namespace healthcareonaspdotnet.Service;

public interface IAuthorizationService {

    Task Create(Authorization model , CancellationToken cancellationToken);
    Task<bool> Update(Authorization model, CancellationToken cancellationToken);
    Task<Authorization?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Authorization>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignCoverage(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCoverage(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignOrder(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignOrder(AssociationRequest request, CancellationToken cancellationToken);


}

public class AuthorizationService : IAuthorizationService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IAuthorizationRepository _repository;
    private readonly ILogger<AuthorizationService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public AuthorizationService(
        ApplicationTelemetry telemetry,
        IAuthorizationRepository repository,
        ILogger<AuthorizationService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(Authorization model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "Authorization",
                "CreateAuthorization",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(Authorization model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.AuthNumber = model.AuthNumber;
            existing.RequestedService = model.RequestedService;
            existing.Status = model.Status;

            await _telemetry.Execute(
                "Authorization",
                "UpdateAuthorization",
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

    public Task<Authorization?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Authorization>> GetAll(CancellationToken cancellationToken)
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
                "Authorization",
                "UpdateAuthorization",
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

    public async Task<bool> AssignCoverage(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Authorization found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<CoverageService>().Get(childRequest, cancellationToken);
            parent.Coverage = child;
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

    public async Task<bool> UnassignCoverage(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Authorization found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Coverage = null;
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

    public async Task<bool> AssignOrder(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Authorization found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<ClinicalOrderService>().Get(childRequest, cancellationToken);
            parent.Order = child;
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

    public async Task<bool> UnassignOrder(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Authorization found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Order = null;
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




}
