
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class LabResult
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? LabresultId { get; set; } 
 public virtual string? ResultCode { get; set; } 
 public virtual DateTime? IssuedDate { get; set; } 
public virtual LaboratoryOrder? LaboratoryOrder { get; set; } 
public virtual ICollection<Observation> Observations { get; set; } = new List<Observation>();
public virtual Laboratory? Laboratory { get; set; } 
 public virtual ResultStatus? Status { get; set; } 

    public static LabResult FromRequest(LabResultRequest request) {
        return new LabResult {
            Id = request.Id,
            ResultCode = request.ResultCode,
            IssuedDate = request.IssuedDate,
            Status = request.Status,
        };
    }
}
