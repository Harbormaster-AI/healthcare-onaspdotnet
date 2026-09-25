
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class MedicationDispense
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? MedicationdispenseId { get; set; } 
 public virtual string? DispenseNumber { get; set; } 
 public virtual decimal? Quantity { get; set; } 
 public virtual DateTime? WhenPrepared { get; set; } 
public virtual MedicationOrder? MedicationOrder { get; set; } 
public virtual Pharmacy? Pharmacy { get; set; } 
public virtual Patient? Patient { get; set; } 
 public virtual DispenseStatus? Status { get; set; } 

    public static MedicationDispense FromRequest(MedicationDispenseRequest request) {
        return new MedicationDispense {
            Id = request.Id,
            DispenseNumber = request.DispenseNumber,
            Quantity = request.Quantity,
            WhenPrepared = request.WhenPrepared,
            Status = request.Status,
        };
    }
}
