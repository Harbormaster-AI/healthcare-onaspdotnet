
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Persistence;
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Telemetry;

namespace healthcareonaspdotnet.Service;

public interface IMedicalSupplierService {

    Task Create(MedicalSupplier model , CancellationToken cancellationToken);
    Task<bool> Update(MedicalSupplier model, CancellationToken cancellationToken);
    Task<MedicalSupplier?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<MedicalSupplier>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToFacilities(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromFacilities(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToInventoryItems(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromInventoryItems(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class MedicalSupplierService : IMedicalSupplierService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IMedicalSupplierRepository _repository;
    private readonly ILogger<MedicalSupplierService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public MedicalSupplierService(
        ApplicationTelemetry telemetry,
        IMedicalSupplierRepository repository,
        ILogger<MedicalSupplierService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(MedicalSupplier model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "MedicalSupplier",
                "CreateMedicalSupplier",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(MedicalSupplier model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.Website = model.Website;
            existing.SupplierTier = model.SupplierTier;

            await _telemetry.Execute(
                "MedicalSupplier",
                "UpdateMedicalSupplier",
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

    public Task<MedicalSupplier?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<MedicalSupplier>> GetAll(CancellationToken cancellationToken)
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
                "MedicalSupplier",
                "UpdateMedicalSupplier",
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


    public async Task<bool> AddToFacilities(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "MedicalSupplier",
                "AddToFacilities",
                () => _repository.AddToFacilitiesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromFacilities(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "MedicalSupplier",
                "RemoveFromFacilities",
                () => _repository.RemoveFromFacilitiesAsync(request, cancellationToken));
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

    public async Task<bool> AddToInventoryItems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "MedicalSupplier",
                "AddToInventoryItems",
                () => _repository.AddToInventoryItemsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromInventoryItems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "MedicalSupplier",
                "RemoveFromInventoryItems",
                () => _repository.RemoveFromInventoryItemsAsync(request, cancellationToken));
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
