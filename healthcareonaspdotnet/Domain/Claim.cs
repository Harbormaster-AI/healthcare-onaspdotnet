
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class Claim
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ClaimId { get; set; } 
 public virtual string? ClaimNumber { get; set; } 
 public virtual Money? TotalAmount { get; set; } 
public virtual Patient? Patient { get; set; } 
public virtual Coverage? Coverage { get; set; } 
public virtual Encounter? Encounter { get; set; } 
public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
public virtual InsurancePayer? Payer { get; set; } 
 public virtual ClaimStatus? Status { get; set; } 

    public static Claim FromRequest(ClaimRequest request) {
        return new Claim {
            Id = request.Id,
            ClaimNumber = request.ClaimNumber,
            TotalAmount = request.TotalAmount,
            Status = request.Status,
        };
    }
}
