
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class Pharmacy
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PharmacyId { get; set; } 
 public virtual string? Name { get; set; } 
public virtual Facility? Facility { get; set; } 
public virtual ICollection<MedicationDispense> MedicationDispenses { get; set; } = new List<MedicationDispense>();
public virtual ICollection<MedicationOrder> MedicationOrders { get; set; } = new List<MedicationOrder>();

    public static Pharmacy FromRequest(PharmacyRequest request) {
        return new Pharmacy {
            Id = request.Id,
            Name = request.Name,
        };
    }
}
