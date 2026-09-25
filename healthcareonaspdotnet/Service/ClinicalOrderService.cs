
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Persistence;
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Telemetry;

namespace healthcareonaspdotnet.Service;

public interface IClinicalOrderService {

    Task Create(ClinicalOrder model , CancellationToken cancellationToken);
    Task<bool> Update(ClinicalOrder model, CancellationToken cancellationToken);
    Task<ClinicalOrder?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<ClinicalOrder>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignPatient(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPatient(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignEncounter(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignEncounter(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignOrderingClinician(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignOrderingClinician(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToMedicationOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromMedicationOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToLaboratoryOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromLaboratoryOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToImagingOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromImagingOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToProcedureOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromProcedureOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToAuthorizations(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAuthorizations(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class ClinicalOrderService : IClinicalOrderService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IClinicalOrderRepository _repository;
    private readonly ILogger<ClinicalOrderService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public ClinicalOrderService(
        ApplicationTelemetry telemetry,
        IClinicalOrderRepository repository,
        ILogger<ClinicalOrderService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(ClinicalOrder model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "ClinicalOrder",
                "CreateClinicalOrder",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(ClinicalOrder model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.OrderNumber = model.OrderNumber;
            existing.Status = model.Status;
            existing.OrderType = model.OrderType;
            existing.Priority = model.Priority;

            await _telemetry.Execute(
                "ClinicalOrder",
                "UpdateClinicalOrder",
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

    public Task<ClinicalOrder?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<ClinicalOrder>> GetAll(CancellationToken cancellationToken)
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
                "ClinicalOrder",
                "UpdateClinicalOrder",
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
            _logger.LogError("No ClinicalOrder found using Id {ParentId}", request.ParentId);
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
            _logger.LogError("No ClinicalOrder found using Id {ParentId}", request.ParentId);
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

    public async Task<bool> AssignEncounter(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No ClinicalOrder found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<EncounterService>().Get(childRequest, cancellationToken);
            parent.Encounter = child;
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

    public async Task<bool> UnassignEncounter(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No ClinicalOrder found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Encounter = null;
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

    public async Task<bool> AssignOrderingClinician(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No ClinicalOrder found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<ClinicianService>().Get(childRequest, cancellationToken);
            parent.OrderingClinician = child;
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

    public async Task<bool> UnassignOrderingClinician(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No ClinicalOrder found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.OrderingClinician = null;
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


    public async Task<bool> AddToMedicationOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ClinicalOrder",
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
                "ClinicalOrder",
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

    public async Task<bool> AddToLaboratoryOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ClinicalOrder",
                "AddToLaboratoryOrders",
                () => _repository.AddToLaboratoryOrdersAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromLaboratoryOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ClinicalOrder",
                "RemoveFromLaboratoryOrders",
                () => _repository.RemoveFromLaboratoryOrdersAsync(request, cancellationToken));
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
                "ClinicalOrder",
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
                "ClinicalOrder",
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

    public async Task<bool> AddToProcedureOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ClinicalOrder",
                "AddToProcedureOrders",
                () => _repository.AddToProcedureOrdersAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromProcedureOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ClinicalOrder",
                "RemoveFromProcedureOrders",
                () => _repository.RemoveFromProcedureOrdersAsync(request, cancellationToken));
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

    public async Task<bool> AddToAuthorizations(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ClinicalOrder",
                "AddToAuthorizations",
                () => _repository.AddToAuthorizationsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromAuthorizations(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ClinicalOrder",
                "RemoveFromAuthorizations",
                () => _repository.RemoveFromAuthorizationsAsync(request, cancellationToken));
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
