
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class Discharge
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? DischargeId { get; set; } 
 public virtual DateTime? DischargeDateTime { get; set; } 
public virtual Encounter? Encounter { get; set; } 
 public virtual DischargeDisposition? Disposition { get; set; } 

    public static Discharge FromRequest(DischargeRequest request) {
        return new Discharge {
            Id = request.Id,
            DischargeDateTime = request.DischargeDateTime,
            Disposition = request.Disposition,
        };
    }
}
