
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class HealthSystem
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? HealthsystemId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? LegalName { get; set; } 
 public virtual string? HeadquartersCountry { get; set; } 
 public virtual string? Website { get; set; } 
public virtual ICollection<Facility> Facilities { get; set; } = new List<Facility>();
public virtual ICollection<MedicalSupplier> Suppliers { get; set; } = new List<MedicalSupplier>();

    public static HealthSystem FromRequest(HealthSystemRequest request) {
        return new HealthSystem {
            Id = request.Id,
            Name = request.Name,
            LegalName = request.LegalName,
            HeadquartersCountry = request.HeadquartersCountry,
            Website = request.Website,
        };
    }
}
