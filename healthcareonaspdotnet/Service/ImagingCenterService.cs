
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Persistence;
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Telemetry;

namespace healthcareonaspdotnet.Service;

public interface IImagingCenterService {

    Task Create(ImagingCenter model , CancellationToken cancellationToken);
    Task<bool> Update(ImagingCenter model, CancellationToken cancellationToken);
    Task<ImagingCenter?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<ImagingCenter>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignFacility(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignFacility(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToImagingOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromImagingOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToImagingReports(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromImagingReports(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class ImagingCenterService : IImagingCenterService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IImagingCenterRepository _repository;
    private readonly ILogger<ImagingCenterService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public ImagingCenterService(
        ApplicationTelemetry telemetry,
        IImagingCenterRepository repository,
        ILogger<ImagingCenterService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(ImagingCenter model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "ImagingCenter",
                "CreateImagingCenter",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(ImagingCenter model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;

            await _telemetry.Execute(
                "ImagingCenter",
                "UpdateImagingCenter",
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

    public Task<ImagingCenter?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<ImagingCenter>> GetAll(CancellationToken cancellationToken)
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
                "ImagingCenter",
                "UpdateImagingCenter",
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

    public async Task<bool> AssignFacility(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No ImagingCenter found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<FacilityService>().Get(childRequest, cancellationToken);
            parent.Facility = child;
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

    public async Task<bool> UnassignFacility(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No ImagingCenter found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Facility = null;
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


    public async Task<bool> AddToImagingOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ImagingCenter",
                "AddToImagingOrders",
                () => _repository.AddToImagingOrdersAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromImagingOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ImagingCenter",
                "RemoveFromImagingOrders",
                () => _repository.RemoveFromImagingOrdersAsync(request, cancellationToken));
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

    public async Task<bool> AddToImagingReports(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ImagingCenter",
                "AddToImagingReports",
                () => _repository.AddToImagingReportsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromImagingReports(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ImagingCenter",
                "RemoveFromImagingReports",
                () => _repository.RemoveFromImagingReportsAsync(request, cancellationToken));
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
