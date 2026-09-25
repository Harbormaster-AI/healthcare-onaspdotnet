
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class ImagingOrder
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ImagingorderId { get; set; } 
 public virtual string? BodySite { get; set; } 
 public virtual bool? Contrast { get; set; } 
public virtual ClinicalOrder? Order { get; set; } 
public virtual ImagingCenter? ImagingCenter { get; set; } 
public virtual ICollection<ImagingReport> Reports { get; set; } = new List<ImagingReport>();
 public virtual ImagingModality? Modality { get; set; } 

    public static ImagingOrder FromRequest(ImagingOrderRequest request) {
        return new ImagingOrder {
            Id = request.Id,
            BodySite = request.BodySite,
            Contrast = request.Contrast,
            Modality = request.Modality,
        };
    }
}
