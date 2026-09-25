
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class CareTask
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? CaretaskId { get; set; } 
 public virtual string? Description { get; set; } 
 public virtual DateOnly? DueDate { get; set; } 
public virtual CarePlan? CarePlan { get; set; } 
public virtual Clinician? AssignedTo { get; set; } 
public virtual Encounter? Encounter { get; set; } 
 public virtual TaskStatus_? Status { get; set; } 
 public virtual Priority? Priority { get; set; } 

    public static CareTask FromRequest(CareTaskRequest request) {
        return new CareTask {
            Id = request.Id,
            Description = request.Description,
            DueDate = request.DueDate,
            Status = request.Status,
            Priority = request.Priority,
        };
    }
}
