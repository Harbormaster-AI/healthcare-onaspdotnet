
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class Diagnosis
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? DiagnosisId { get; set; } 
 public virtual string? Code { get; set; } 
 public virtual string? Description { get; set; } 
 public virtual DateOnly? OnsetDate { get; set; } 
public virtual Encounter? Encounter { get; set; } 
public virtual Patient? Patient { get; set; } 
 public virtual DiagnosisCertainty? Certainty { get; set; } 

    public static Diagnosis FromRequest(DiagnosisRequest request) {
        return new Diagnosis {
            Id = request.Id,
            Code = request.Code,
            Description = request.Description,
            OnsetDate = request.OnsetDate,
            Certainty = request.Certainty,
        };
    }
}
