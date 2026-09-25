
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Persistence;
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Telemetry;

namespace healthcareonaspdotnet.Service;

public interface ICarePlanService {

    Task Create(CarePlan model , CancellationToken cancellationToken);
    Task<bool> Update(CarePlan model, CancellationToken cancellationToken);
    Task<CarePlan?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<CarePlan>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignPatient(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPatient(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignCareTeam(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCareTeam(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToEncounters(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromEncounters(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToTasks(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromTasks(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class CarePlanService : ICarePlanService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly ICarePlanRepository _repository;
    private readonly ILogger<CarePlanService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public CarePlanService(
        ApplicationTelemetry telemetry,
        ICarePlanRepository repository,
        ILogger<CarePlanService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(CarePlan model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "CarePlan",
                "CreateCarePlan",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(CarePlan model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.PlanNumber = model.PlanNumber;
            existing.GoalSummary = model.GoalSummary;
            existing.Status = model.Status;

            await _telemetry.Execute(
                "CarePlan",
                "UpdateCarePlan",
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

    public Task<CarePlan?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<CarePlan>> GetAll(CancellationToken cancellationToken)
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
                "CarePlan",
                "UpdateCarePlan",
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
            _logger.LogError("No CarePlan found using Id {ParentId}", request.ParentId);
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
            _logger.LogError("No CarePlan found using Id {ParentId}", request.ParentId);
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

    public async Task<bool> AssignCareTeam(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No CarePlan found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<CareTeamService>().Get(childRequest, cancellationToken);
            parent.CareTeam = child;
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

    public async Task<bool> UnassignCareTeam(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No CarePlan found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.CareTeam = null;
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


    public async Task<bool> AddToEncounters(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "CarePlan",
                "AddToEncounters",
                () => _repository.AddToEncountersAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromEncounters(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "CarePlan",
                "RemoveFromEncounters",
                () => _repository.RemoveFromEncountersAsync(request, cancellationToken));
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

    public async Task<bool> AddToTasks(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "CarePlan",
                "AddToTasks",
                () => _repository.AddToTasksAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromTasks(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "CarePlan",
                "RemoveFromTasks",
                () => _repository.RemoveFromTasksAsync(request, cancellationToken));
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
