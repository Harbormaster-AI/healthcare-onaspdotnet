
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class SoftwareUpdate
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? SoftwareupdateId { get; set; } 
 public virtual string? Version { get; set; } 
 public virtual DateTime? AppliedDate { get; set; } 
public virtual MedicalDevice? Device { get; set; } 
 public virtual SoftwareUpdateType? UpdateType { get; set; } 

    public static SoftwareUpdate FromRequest(SoftwareUpdateRequest request) {
        return new SoftwareUpdate {
            Id = request.Id,
            Version = request.Version,
            AppliedDate = request.AppliedDate,
            UpdateType = request.UpdateType,
        };
    }
}
