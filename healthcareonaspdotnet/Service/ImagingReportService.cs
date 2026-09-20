using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Persistence;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Service;

public interface IImagingReportService {

    Task Create(ImagingReport model , CancellationToken cancellationToken);
    Task<bool> Update(ImagingReport model, CancellationToken cancellationToken);
    Task<ImagingReport?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<ImagingReport>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignImagingOrder(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignImagingOrder(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignClinician(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignClinician(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignEncounter(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignEncounter(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignImagingCenter(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignImagingCenter(AssociationRequest request, CancellationToken cancellationToken);


}

public class ImagingReportService : IImagingReportService
{
    private readonly IImagingReportRepository _repository;
    private readonly ILogger<ImagingReportService> _logger;

    public ImagingReportService(
        IImagingReportRepository repository, ILogger<ImagingReportService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(ImagingReport model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(ImagingReport model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.ReportNumber = model.ReportNumber;
            existing.Impression = model.Impression;
            existing.ReportedDate = model.ReportedDate;
            existing.Status = model.Status;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<ImagingReport?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<ImagingReport>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignImagingOrder(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignImagingOrder(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignClinician(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignClinician(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignEncounter(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignEncounter(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignImagingCenter(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignImagingCenter(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
