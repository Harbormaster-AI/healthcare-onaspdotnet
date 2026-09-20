using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Persistence;
using healthcareonaspdotnet.Contracts;

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
    private readonly IFacilityRepository _repository;
    private readonly ILogger<FacilityService> _logger;

    public FacilityService(
        IFacilityRepository repository, ILogger<FacilityService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Facility model, CancellationToken cancellationToken)
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

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
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
            await _repository.DeleteAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;

    }

    public async Task<bool> AssignHealthSystem(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignHealthSystem(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToDepartments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromDepartments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToCareTeams(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromCareTeams(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToLaboratories(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromLaboratories(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToImagingCenters(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromImagingCenters(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToPharmacies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromPharmacies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToInventoryItems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromInventoryItems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
