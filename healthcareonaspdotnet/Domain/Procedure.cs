
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class Procedure
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ProcedureId { get; set; } 
 public virtual string? ProcedureCode { get; set; } 
 public virtual DateTime? StartDateTime { get; set; } 
 public virtual DateTime? EndDateTime { get; set; } 
public virtual Encounter? Encounter { get; set; } 
public virtual Clinician? Performer { get; set; } 
public virtual ProcedureOrder? ProcedureOrder { get; set; } 
 public virtual ProcedureStatus? Status { get; set; } 

    public static Procedure FromRequest(ProcedureRequest request) {
        return new Procedure {
            Id = request.Id,
            ProcedureCode = request.ProcedureCode,
            StartDateTime = request.StartDateTime,
            EndDateTime = request.EndDateTime,
            Status = request.Status,
        };
    }
}
