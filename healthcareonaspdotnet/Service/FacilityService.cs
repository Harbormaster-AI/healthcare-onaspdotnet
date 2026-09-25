
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Persistence;
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Telemetry;

namespace healthcareonaspdotnet.Service;

public interface IFacilityService {

    Task Create(Facility model , CancellationToken cancellationToken);
    Task<bool> Update(Facility model, CancellationToken cancellationToken);
    Task<Facility?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Facility>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignHealthSystem(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignHealthSystem(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToDepartments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDepartments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToCareTeams(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCareTeams(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToLaboratories(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromLaboratories(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToImagingCenters(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromImagingCenters(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToPharmacies(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPharmacies(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToInventoryItems(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromInventoryItems(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class FacilityService : IFacilityService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IFacilityRepository _repository;
    private readonly ILogger<FacilityService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public FacilityService(
        ApplicationTelemetry telemetry,
        IFacilityRepository repository,
        ILogger<FacilityService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(Facility model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "Facility",
                "CreateFacility",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(Facility model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.FacilityCode = model.FacilityCode;
            existing.Address = model.Address;
            existing.FacilityType = model.FacilityType;

            await _telemetry.Execute(
                "Facility",
                "UpdateFacility",
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

    public Task<Facility?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Facility>> GetAll(CancellationToken cancellationToken)
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
                "Facility",
                "UpdateFacility",
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

    public async Task<bool> AssignHealthSystem(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Facility found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<HealthSystemService>().Get(childRequest, cancellationToken);
            parent.HealthSystem = child;
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

    public async Task<bool> UnassignHealthSystem(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Facility found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.HealthSystem = null;
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


    public async Task<bool> AddToDepartments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Facility",
                "AddToDepartments",
                () => _repository.AddToDepartmentsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromDepartments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Facility",
                "RemoveFromDepartments",
                () => _repository.RemoveFromDepartmentsAsync(request, cancellationToken));
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

    public async Task<bool> AddToCareTeams(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Facility",
                "AddToCareTeams",
                () => _repository.AddToCareTeamsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromCareTeams(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Facility",
                "RemoveFromCareTeams",
                () => _repository.RemoveFromCareTeamsAsync(request, cancellationToken));
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

    public async Task<bool> AddToLaboratories(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Facility",
                "AddToLaboratories",
                () => _repository.AddToLaboratoriesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromLaboratories(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Facility",
                "RemoveFromLaboratories",
                () => _repository.RemoveFromLaboratoriesAsync(request, cancellationToken));
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

    public async Task<bool> AddToImagingCenters(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Facility",
                "AddToImagingCenters",
                () => _repository.AddToImagingCentersAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromImagingCenters(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Facility",
                "RemoveFromImagingCenters",
                () => _repository.RemoveFromImagingCentersAsync(request, cancellationToken));
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

    public async Task<bool> AddToPharmacies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Facility",
                "AddToPharmacies",
                () => _repository.AddToPharmaciesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromPharmacies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Facility",
                "RemoveFromPharmacies",
                () => _repository.RemoveFromPharmaciesAsync(request, cancellationToken));
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
                "Facility",
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
                "Facility",
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
