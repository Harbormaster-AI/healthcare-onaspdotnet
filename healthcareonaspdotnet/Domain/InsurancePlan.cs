
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class InsurancePlan
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? InsuranceplanId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? PlanCode { get; set; } 
public virtual InsurancePayer? Payer { get; set; } 
public virtual ICollection<Coverage> Coverages { get; set; } = new List<Coverage>();
 public virtual InsurancePlanType? PlanType { get; set; } 

    public static InsurancePlan FromRequest(InsurancePlanRequest request) {
        return new InsurancePlan {
            Id = request.Id,
            Name = request.Name,
            PlanCode = request.PlanCode,
            PlanType = request.PlanType,
        };
    }
}
