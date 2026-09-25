
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Persistence;
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Telemetry;

namespace healthcareonaspdotnet.Service;

public interface IPatientService {

    Task Create(Patient model , CancellationToken cancellationToken);
    Task<bool> Update(Patient model, CancellationToken cancellationToken);
    Task<Patient?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Patient>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToAppointments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAppointments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToEncounters(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromEncounters(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToCarePlans(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCarePlans(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToAllergies(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAllergies(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToConditions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromConditions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToMedicationOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromMedicationOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToLabOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromLabOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToImagingOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromImagingOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToCoverages(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCoverages(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToClaims(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromClaims(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToDevices(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDevices(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToObservations(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromObservations(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class PatientService : IPatientService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IPatientRepository _repository;
    private readonly ILogger<PatientService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public PatientService(
        ApplicationTelemetry telemetry,
        IPatientRepository repository,
        ILogger<PatientService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(Patient model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "Patient",
                "CreatePatient",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(Patient model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.FirstName = model.FirstName;
            existing.LastName = model.LastName;
            existing.Mrn = model.Mrn;
            existing.DateOfBirth = model.DateOfBirth;
            existing.Address = model.Address;
            existing.PrimaryLanguage = model.PrimaryLanguage;
            existing.SexAtBirth = model.SexAtBirth;
            existing.BloodType = model.BloodType;

            await _telemetry.Execute(
                "Patient",
                "UpdatePatient",
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

    public Task<Patient?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Patient>> GetAll(CancellationToken cancellationToken)
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
                "Patient",
                "UpdatePatient",
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


    public async Task<bool> AddToAppointments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Patient",
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
                "Patient",
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
                "Patient",
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
                "Patient",
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

    public async Task<bool> AddToCarePlans(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Patient",
                "AddToCarePlans",
                () => _repository.AddToCarePlansAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromCarePlans(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Patient",
                "RemoveFromCarePlans",
                () => _repository.RemoveFromCarePlansAsync(request, cancellationToken));
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

    public async Task<bool> AddToAllergies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Patient",
                "AddToAllergies",
                () => _repository.AddToAllergiesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromAllergies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Patient",
                "RemoveFromAllergies",
                () => _repository.RemoveFromAllergiesAsync(request, cancellationToken));
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

    public async Task<bool> AddToConditions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Patient",
                "AddToConditions",
                () => _repository.AddToConditionsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromConditions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Patient",
                "RemoveFromConditions",
                () => _repository.RemoveFromConditionsAsync(request, cancellationToken));
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
                "Patient",
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
                "Patient",
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

    public async Task<bool> AddToLabOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Patient",
                "AddToLabOrders",
                () => _repository.AddToLabOrdersAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromLabOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Patient",
                "RemoveFromLabOrders",
                () => _repository.RemoveFromLabOrdersAsync(request, cancellationToken));
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
                "Patient",
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
                "Patient",
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

    public async Task<bool> AddToCoverages(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Patient",
                "AddToCoverages",
                () => _repository.AddToCoveragesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromCoverages(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Patient",
                "RemoveFromCoverages",
                () => _repository.RemoveFromCoveragesAsync(request, cancellationToken));
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

    public async Task<bool> AddToClaims(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Patient",
                "AddToClaims",
                () => _repository.AddToClaimsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromClaims(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Patient",
                "RemoveFromClaims",
                () => _repository.RemoveFromClaimsAsync(request, cancellationToken));
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

    public async Task<bool> AddToDevices(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Patient",
                "AddToDevices",
                () => _repository.AddToDevicesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromDevices(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Patient",
                "RemoveFromDevices",
                () => _repository.RemoveFromDevicesAsync(request, cancellationToken));
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
                "Patient",
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
                "Patient",
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



}
