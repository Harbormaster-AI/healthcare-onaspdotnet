
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class ImagingCenter
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ImagingcenterId { get; set; } 
 public virtual string? Name { get; set; } 
public virtual Facility? Facility { get; set; } 
public virtual ICollection<ImagingOrder> ImagingOrders { get; set; } = new List<ImagingOrder>();
public virtual ICollection<ImagingReport> ImagingReports { get; set; } = new List<ImagingReport>();

    public static ImagingCenter FromRequest(ImagingCenterRequest request) {
        return new ImagingCenter {
            Id = request.Id,
            Name = request.Name,
        };
    }
}
