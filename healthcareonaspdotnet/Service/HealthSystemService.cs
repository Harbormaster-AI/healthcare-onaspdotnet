
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Persistence;
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Telemetry;

namespace healthcareonaspdotnet.Service;

public interface IHealthSystemService {

    Task Create(HealthSystem model , CancellationToken cancellationToken);
    Task<bool> Update(HealthSystem model, CancellationToken cancellationToken);
    Task<HealthSystem?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<HealthSystem>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToFacilities(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromFacilities(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToSuppliers(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromSuppliers(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class HealthSystemService : IHealthSystemService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IHealthSystemRepository _repository;
    private readonly ILogger<HealthSystemService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public HealthSystemService(
        ApplicationTelemetry telemetry,
        IHealthSystemRepository repository,
        ILogger<HealthSystemService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(HealthSystem model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "HealthSystem",
                "CreateHealthSystem",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(HealthSystem model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.LegalName = model.LegalName;
            existing.HeadquartersCountry = model.HeadquartersCountry;
            existing.Website = model.Website;

            await _telemetry.Execute(
                "HealthSystem",
                "UpdateHealthSystem",
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

    public Task<HealthSystem?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<HealthSystem>> GetAll(CancellationToken cancellationToken)
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
                "HealthSystem",
                "UpdateHealthSystem",
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
                "HealthSystem",
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
                "HealthSystem",
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

    public async Task<bool> AddToSuppliers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "HealthSystem",
                "AddToSuppliers",
                () => _repository.AddToSuppliersAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromSuppliers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "HealthSystem",
                "RemoveFromSuppliers",
                () => _repository.RemoveFromSuppliersAsync(request, cancellationToken));
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
