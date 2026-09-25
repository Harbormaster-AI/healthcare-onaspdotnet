
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class Allergy
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? AllergyId { get; set; } 
 public virtual string? Substance { get; set; } 
 public virtual string? Reaction { get; set; } 
public virtual Patient? Patient { get; set; } 
 public virtual AllergySeverity? Severity { get; set; } 
 public virtual AllergyStatus? Status { get; set; } 

    public static Allergy FromRequest(AllergyRequest request) {
        return new Allergy {
            Id = request.Id,
            Substance = request.Substance,
            Reaction = request.Reaction,
            Severity = request.Severity,
            Status = request.Status,
        };
    }
}
