
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Persistence;
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Telemetry;

namespace healthcareonaspdotnet.Service;

public interface IPharmacyService {

    Task Create(Pharmacy model , CancellationToken cancellationToken);
    Task<bool> Update(Pharmacy model, CancellationToken cancellationToken);
    Task<Pharmacy?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Pharmacy>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignFacility(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignFacility(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToMedicationDispenses(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromMedicationDispenses(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToMedicationOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromMedicationOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class PharmacyService : IPharmacyService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IPharmacyRepository _repository;
    private readonly ILogger<PharmacyService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public PharmacyService(
        ApplicationTelemetry telemetry,
        IPharmacyRepository repository,
        ILogger<PharmacyService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(Pharmacy model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "Pharmacy",
                "CreatePharmacy",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(Pharmacy model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;

            await _telemetry.Execute(
                "Pharmacy",
                "UpdatePharmacy",
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

    public Task<Pharmacy?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Pharmacy>> GetAll(CancellationToken cancellationToken)
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
                "Pharmacy",
                "UpdatePharmacy",
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
            _logger.LogError("No Pharmacy found using Id {ParentId}", request.ParentId);
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
            _logger.LogError("No Pharmacy found using Id {ParentId}", request.ParentId);
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


    public async Task<bool> AddToMedicationDispenses(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Pharmacy",
                "AddToMedicationDispenses",
                () => _repository.AddToMedicationDispensesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromMedicationDispenses(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Pharmacy",
                "RemoveFromMedicationDispenses",
                () => _repository.RemoveFromMedicationDispensesAsync(request, cancellationToken));
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

    public async Task<bool> AddToMedicationOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Pharmacy",
                "AddToMedicationOrders",
                () => _repository.AddToMedicationOrdersAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromMedicationOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Pharmacy",
                "RemoveFromMedicationOrders",
                () => _repository.RemoveFromMedicationOrdersAsync(request, cancellationToken));
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
