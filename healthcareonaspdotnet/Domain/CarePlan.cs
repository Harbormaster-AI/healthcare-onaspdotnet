
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class CarePlan
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? CareplanId { get; set; } 
 public virtual string? PlanNumber { get; set; } 
 public virtual string? GoalSummary { get; set; } 
public virtual Patient? Patient { get; set; } 
public virtual ICollection<Encounter> Encounters { get; set; } = new List<Encounter>();
public virtual ICollection<CareTask> Tasks { get; set; } = new List<CareTask>();
public virtual CareTeam? CareTeam { get; set; } 
 public virtual CarePlanStatus? Status { get; set; } 

    public static CarePlan FromRequest(CarePlanRequest request) {
        return new CarePlan {
            Id = request.Id,
            PlanNumber = request.PlanNumber,
            GoalSummary = request.GoalSummary,
            Status = request.Status,
        };
    }
}
