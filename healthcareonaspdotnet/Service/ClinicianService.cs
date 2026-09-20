using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Persistence;
using healthcareonaspdotnet.Contracts;

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
    private readonly IClinicianRepository _repository;
    private readonly ILogger<ClinicianService> _logger;

    public ClinicianService(
        IClinicianRepository repository, ILogger<ClinicianService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Clinician model, CancellationToken cancellationToken)
    {
        try
        {
            await _repository.AddAsync(model, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
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

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
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
            await _repository.DeleteAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;

    }


    public async Task<bool> AddToCareTeams(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromCareTeams(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToAppointments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromAppointments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToEncounters(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromEncounters(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToProcedures(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromProcedures(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToImagingReports(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromImagingReports(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
