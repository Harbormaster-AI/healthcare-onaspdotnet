
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class LaboratoryOrder
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? LaboratoryorderId { get; set; } 
 public virtual string? TestCode { get; set; } 
 public virtual bool? FastingRequired { get; set; } 
public virtual ClinicalOrder? Order { get; set; } 
public virtual Laboratory? Laboratory { get; set; } 
public virtual ICollection<LabResult> Results { get; set; } = new List<LabResult>();
 public virtual SpecimenType? SpecimenType { get; set; } 

    public static LaboratoryOrder FromRequest(LaboratoryOrderRequest request) {
        return new LaboratoryOrder {
            Id = request.Id,
            TestCode = request.TestCode,
            FastingRequired = request.FastingRequired,
            SpecimenType = request.SpecimenType,
        };
    }
}
