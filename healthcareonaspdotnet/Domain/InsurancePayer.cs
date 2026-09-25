
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class InsurancePayer
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? InsurancepayerId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Website { get; set; } 
public virtual ICollection<InsurancePlan> Plans { get; set; } = new List<InsurancePlan>();
public virtual ICollection<Claim> Claims { get; set; } = new List<Claim>();
 public virtual PayerType? PayerType { get; set; } 

    public static InsurancePayer FromRequest(InsurancePayerRequest request) {
        return new InsurancePayer {
            Id = request.Id,
            Name = request.Name,
            Website = request.Website,
            PayerType = request.PayerType,
        };
    }
}
