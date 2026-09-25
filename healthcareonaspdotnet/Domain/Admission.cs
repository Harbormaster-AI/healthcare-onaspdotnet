
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class Admission
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? AdmissionId { get; set; } 
 public virtual DateTime? AdmitDateTime { get; set; } 
 public virtual string? Bed { get; set; } 
public virtual Encounter? Encounter { get; set; } 
public virtual Facility? Facility { get; set; } 
 public virtual AdmissionType? AdmissionType { get; set; } 

    public static Admission FromRequest(AdmissionRequest request) {
        return new Admission {
            Id = request.Id,
            AdmitDateTime = request.AdmitDateTime,
            Bed = request.Bed,
            AdmissionType = request.AdmissionType,
        };
    }
}
