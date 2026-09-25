
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class MedicalDevice
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? MedicaldeviceId { get; set; } 
 public virtual string? Udi { get; set; } 
 public virtual string? Manufacturer { get; set; } 
public virtual Patient? Patient { get; set; } 
public virtual ICollection<Observation> Observations { get; set; } = new List<Observation>();
public virtual ICollection<SoftwareUpdate> SoftwareUpdates { get; set; } = new List<SoftwareUpdate>();
 public virtual DeviceType? DeviceType { get; set; } 
 public virtual DeviceConnectivityStatus? ConnectivityStatus { get; set; } 

    public static MedicalDevice FromRequest(MedicalDeviceRequest request) {
        return new MedicalDevice {
            Id = request.Id,
            Udi = request.Udi,
            Manufacturer = request.Manufacturer,
            DeviceType = request.DeviceType,
            ConnectivityStatus = request.ConnectivityStatus,
        };
    }
}
