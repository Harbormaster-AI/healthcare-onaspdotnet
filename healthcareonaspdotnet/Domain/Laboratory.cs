
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class Laboratory
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? LaboratoryId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? CliaNumber { get; set; } 
public virtual Facility? Facility { get; set; } 
public virtual ICollection<LaboratoryOrder> LaboratoryOrders { get; set; } = new List<LaboratoryOrder>();
public virtual ICollection<LabResult> LabResults { get; set; } = new List<LabResult>();

    public static Laboratory FromRequest(LaboratoryRequest request) {
        return new Laboratory {
            Id = request.Id,
            Name = request.Name,
            CliaNumber = request.CliaNumber,
        };
    }
}
