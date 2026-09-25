
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class ProcedureOrder
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ProcedureorderId { get; set; } 
 public virtual string? ProcedureCode { get; set; } 
 public virtual bool? ConsentObtained { get; set; } 
public virtual ClinicalOrder? Order { get; set; } 
public virtual Facility? Facility { get; set; } 
public virtual Procedure? Procedure { get; set; } 
 public virtual AnesthesiaType? AnesthesiaType { get; set; } 

    public static ProcedureOrder FromRequest(ProcedureOrderRequest request) {
        return new ProcedureOrder {
            Id = request.Id,
            ProcedureCode = request.ProcedureCode,
            ConsentObtained = request.ConsentObtained,
            AnesthesiaType = request.AnesthesiaType,
        };
    }
}
