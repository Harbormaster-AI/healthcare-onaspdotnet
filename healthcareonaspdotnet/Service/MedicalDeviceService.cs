
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Persistence;
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Telemetry;

namespace healthcareonaspdotnet.Service;

public interface IMedicalDeviceService {

    Task Create(MedicalDevice model , CancellationToken cancellationToken);
    Task<bool> Update(MedicalDevice model, CancellationToken cancellationToken);
    Task<MedicalDevice?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<MedicalDevice>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignPatient(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPatient(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToObservations(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromObservations(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToSoftwareUpdates(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromSoftwareUpdates(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class MedicalDeviceService : IMedicalDeviceService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IMedicalDeviceRepository _repository;
    private readonly ILogger<MedicalDeviceService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public MedicalDeviceService(
        ApplicationTelemetry telemetry,
        IMedicalDeviceRepository repository,
        ILogger<MedicalDeviceService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(MedicalDevice model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "MedicalDevice",
                "CreateMedicalDevice",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(MedicalDevice model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Udi = model.Udi;
            existing.Manufacturer = model.Manufacturer;
            existing.DeviceType = model.DeviceType;
            existing.ConnectivityStatus = model.ConnectivityStatus;

            await _telemetry.Execute(
                "MedicalDevice",
                "UpdateMedicalDevice",
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

    public Task<MedicalDevice?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<MedicalDevice>> GetAll(CancellationToken cancellationToken)
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
                "MedicalDevice",
                "UpdateMedicalDevice",
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

    public async Task<bool> AssignPatient(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No MedicalDevice found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<PatientService>().Get(childRequest, cancellationToken);
            parent.Patient = child;
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

    public async Task<bool> UnassignPatient(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No MedicalDevice found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Patient = null;
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
                "MedicalDevice",
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
                "MedicalDevice",
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

    public async Task<bool> AddToSoftwareUpdates(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "MedicalDevice",
                "AddToSoftwareUpdates",
                () => _repository.AddToSoftwareUpdatesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromSoftwareUpdates(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "MedicalDevice",
                "RemoveFromSoftwareUpdates",
                () => _repository.RemoveFromSoftwareUpdatesAsync(request, cancellationToken));
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
