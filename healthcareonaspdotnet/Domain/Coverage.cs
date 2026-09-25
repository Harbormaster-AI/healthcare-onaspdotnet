
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class Coverage
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? CoverageId { get; set; } 
 public virtual string? MemberId { get; set; } 
 public virtual string? GroupNumber { get; set; } 
 public virtual DateOnly? EffectiveDate { get; set; } 
 public virtual DateOnly? EndDate { get; set; } 
public virtual Patient? Patient { get; set; } 
public virtual InsurancePlan? Plan { get; set; } 
public virtual ICollection<Claim> Claims { get; set; } = new List<Claim>();
public virtual ICollection<Authorization> Authorizations { get; set; } = new List<Authorization>();
 public virtual CoverageType? CoverageType { get; set; } 

    public static Coverage FromRequest(CoverageRequest request) {
        return new Coverage {
            Id = request.Id,
            MemberId = request.MemberId,
            GroupNumber = request.GroupNumber,
            EffectiveDate = request.EffectiveDate,
            EndDate = request.EndDate,
            CoverageType = request.CoverageType,
        };
    }
}
