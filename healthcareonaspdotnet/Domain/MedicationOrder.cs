
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class MedicationOrder
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? MedicationorderId { get; set; } 
 public virtual string? MedicationCode { get; set; } 
 public virtual Dose? Dose { get; set; } 
 public virtual string? Frequency { get; set; } 
 public virtual string? Duration { get; set; } 
public virtual ClinicalOrder? Order { get; set; } 
public virtual Pharmacy? Pharmacy { get; set; } 
public virtual ICollection<MedicationDispense> Dispenses { get; set; } = new List<MedicationDispense>();
 public virtual RouteOfAdministration? Route { get; set; } 

    public static MedicationOrder FromRequest(MedicationOrderRequest request) {
        return new MedicationOrder {
            Id = request.Id,
            MedicationCode = request.MedicationCode,
            Dose = request.Dose,
            Frequency = request.Frequency,
            Duration = request.Duration,
            Route = request.Route,
        };
    }
}
