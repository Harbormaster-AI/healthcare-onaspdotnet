
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Persistence;
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Telemetry;

namespace healthcareonaspdotnet.Service;

public interface IClinicianService {

    Task Create(Clinician model , CancellationToken cancellationToken);
    Task<bool> Update(Clinician model, CancellationToken cancellationToken);
    Task<Clinician?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Clinician>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToCareTeams(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCareTeams(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToAppointments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAppointments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToEncounters(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromEncounters(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToProcedures(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromProcedures(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToImagingReports(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromImagingReports(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class ClinicianService : IClinicianService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IClinicianRepository _repository;
    private readonly ILogger<ClinicianService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public ClinicianService(
        ApplicationTelemetry telemetry,
        IClinicianRepository repository,
        ILogger<ClinicianService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(Clinician model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "Clinician",
                "CreateClinician",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(Clinician model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.FirstName = model.FirstName;
            existing.LastName = model.LastName;
            existing.LicenseNumber = model.LicenseNumber;
            existing.ClinicianType = model.ClinicianType;
            existing.Specialty = model.Specialty;

            await _telemetry.Execute(
                "Clinician",
                "UpdateClinician",
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

    public Task<Clinician?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Clinician>> GetAll(CancellationToken cancellationToken)
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
                "Clinician",
                "UpdateClinician",
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


    public async Task<bool> AddToCareTeams(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Clinician",
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
                "Clinician",
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

    public async Task<bool> AddToAppointments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Clinician",
                "AddToAppointments",
                () => _repository.AddToAppointmentsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromAppointments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Clinician",
                "RemoveFromAppointments",
                () => _repository.RemoveFromAppointmentsAsync(request, cancellationToken));
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
                "Clinician",
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
                "Clinician",
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

    public async Task<bool> AddToProcedures(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Clinician",
                "AddToProcedures",
                () => _repository.AddToProceduresAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromProcedures(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Clinician",
                "RemoveFromProcedures",
                () => _repository.RemoveFromProceduresAsync(request, cancellationToken));
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
                "Clinician",
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
                "Clinician",
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
