
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class Observation
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ObservationId { get; set; } 
 public virtual string? Code { get; set; } 
 public virtual string? Value { get; set; } 
 public virtual string? Unit { get; set; } 
 public virtual DateTime? EffectiveDateTime { get; set; } 
public virtual Encounter? Encounter { get; set; } 
public virtual Patient? Patient { get; set; } 
public virtual MedicalDevice? Device { get; set; } 
public virtual LabResult? LabResult { get; set; } 
 public virtual ObservationInterpretation? Interpretation { get; set; } 

    public static Observation FromRequest(ObservationRequest request) {
        return new Observation {
            Id = request.Id,
            Code = request.Code,
            Value = request.Value,
            Unit = request.Unit,
            EffectiveDateTime = request.EffectiveDateTime,
            Interpretation = request.Interpretation,
        };
    }
}
