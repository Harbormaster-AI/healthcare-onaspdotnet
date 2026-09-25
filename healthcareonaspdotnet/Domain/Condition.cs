
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class Condition
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ConditionId { get; set; } 
 public virtual string? Code { get; set; } 
 public virtual DateOnly? OnsetDate { get; set; } 
 public virtual DateOnly? AbatementDate { get; set; } 
public virtual Patient? Patient { get; set; } 
 public virtual ConditionStatus? ClinicalStatus { get; set; } 
 public virtual DiagnosisCertainty? VerificationStatus { get; set; } 

    public static Condition FromRequest(ConditionRequest request) {
        return new Condition {
            Id = request.Id,
            Code = request.Code,
            OnsetDate = request.OnsetDate,
            AbatementDate = request.AbatementDate,
            ClinicalStatus = request.ClinicalStatus,
            VerificationStatus = request.VerificationStatus,
        };
    }
}
