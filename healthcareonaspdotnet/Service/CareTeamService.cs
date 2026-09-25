
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Persistence;
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Telemetry;

namespace healthcareonaspdotnet.Service;

public interface ICareTeamService {

    Task Create(CareTeam model , CancellationToken cancellationToken);
    Task<bool> Update(CareTeam model, CancellationToken cancellationToken);
    Task<CareTeam?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<CareTeam>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignDepartment(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignDepartment(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToClinicians(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromClinicians(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToPatients(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPatients(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class CareTeamService : ICareTeamService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly ICareTeamRepository _repository;
    private readonly ILogger<CareTeamService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public CareTeamService(
        ApplicationTelemetry telemetry,
        ICareTeamRepository repository,
        ILogger<CareTeamService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(CareTeam model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "CareTeam",
                "CreateCareTeam",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(CareTeam model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.CareSetting = model.CareSetting;

            await _telemetry.Execute(
                "CareTeam",
                "UpdateCareTeam",
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

    public Task<CareTeam?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<CareTeam>> GetAll(CancellationToken cancellationToken)
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
                "CareTeam",
                "UpdateCareTeam",
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

    public async Task<bool> AssignDepartment(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No CareTeam found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<DepartmentService>().Get(childRequest, cancellationToken);
            parent.Department = child;
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

    public async Task<bool> UnassignDepartment(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No CareTeam found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Department = null;
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


    public async Task<bool> AddToClinicians(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "CareTeam",
                "AddToClinicians",
                () => _repository.AddToCliniciansAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromClinicians(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "CareTeam",
                "RemoveFromClinicians",
                () => _repository.RemoveFromCliniciansAsync(request, cancellationToken));
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

    public async Task<bool> AddToPatients(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "CareTeam",
                "AddToPatients",
                () => _repository.AddToPatientsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromPatients(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "CareTeam",
                "RemoveFromPatients",
                () => _repository.RemoveFromPatientsAsync(request, cancellationToken));
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
