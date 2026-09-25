
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class ImagingReport
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ImagingreportId { get; set; } 
 public virtual string? ReportNumber { get; set; } 
 public virtual string? Impression { get; set; } 
 public virtual DateTime? ReportedDate { get; set; } 
public virtual ImagingOrder? ImagingOrder { get; set; } 
public virtual Clinician? Clinician { get; set; } 
public virtual Encounter? Encounter { get; set; } 
public virtual ImagingCenter? ImagingCenter { get; set; } 
 public virtual ResultStatus? Status { get; set; } 

    public static ImagingReport FromRequest(ImagingReportRequest request) {
        return new ImagingReport {
            Id = request.Id,
            ReportNumber = request.ReportNumber,
            Impression = request.Impression,
            ReportedDate = request.ReportedDate,
            Status = request.Status,
        };
    }
}
