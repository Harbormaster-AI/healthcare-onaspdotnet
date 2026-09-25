
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Persistence;
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Telemetry;

namespace healthcareonaspdotnet.Service;

public interface ILabResultService {

    Task Create(LabResult model , CancellationToken cancellationToken);
    Task<bool> Update(LabResult model, CancellationToken cancellationToken);
    Task<LabResult?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<LabResult>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignLaboratoryOrder(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignLaboratoryOrder(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignLaboratory(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignLaboratory(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToObservations(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromObservations(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class LabResultService : ILabResultService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly ILabResultRepository _repository;
    private readonly ILogger<LabResultService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public LabResultService(
        ApplicationTelemetry telemetry,
        ILabResultRepository repository,
        ILogger<LabResultService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(LabResult model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "LabResult",
                "CreateLabResult",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(LabResult model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.ResultCode = model.ResultCode;
            existing.IssuedDate = model.IssuedDate;
            existing.Status = model.Status;

            await _telemetry.Execute(
                "LabResult",
                "UpdateLabResult",
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

    public Task<LabResult?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<LabResult>> GetAll(CancellationToken cancellationToken)
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
                "LabResult",
                "UpdateLabResult",
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

    public async Task<bool> AssignLaboratoryOrder(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No LabResult found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<LaboratoryOrderService>().Get(childRequest, cancellationToken);
            parent.LaboratoryOrder = child;
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

    public async Task<bool> UnassignLaboratoryOrder(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No LabResult found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.LaboratoryOrder = null;
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

    public async Task<bool> AssignLaboratory(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No LabResult found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<LaboratoryService>().Get(childRequest, cancellationToken);
            parent.Laboratory = child;
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

    public async Task<bool> UnassignLaboratory(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No LabResult found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Laboratory = null;
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


    public async Task<bool> AddToObservations(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "LabResult",
                "AddToObservations",
                () => _repository.AddToObservationsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromObservations(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "LabResult",
                "RemoveFromObservations",
                () => _repository.RemoveFromObservationsAsync(request, cancellationToken));
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
