using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Persistence;

public interface IMedicalDeviceRepository
{
    Task<MedicalDevice?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<MedicalDevice>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(MedicalDevice medicalDevice, CancellationToken cancellationToken);
    Task UpdateAsync(MedicalDevice medicalDevice, CancellationToken cancellationToken);
    Task DeleteAsync(MedicalDevice medicalDevice, CancellationToken cancellationToken);

    Task AddToObservationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromObservationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToSoftwareUpdatesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromSoftwareUpdatesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
