
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class Facility
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? FacilityId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? FacilityCode { get; set; } 
 public virtual Address? Address { get; set; } 
public virtual HealthSystem? HealthSystem { get; set; } 
public virtual ICollection<Department> Departments { get; set; } = new List<Department>();
public virtual ICollection<CareTeam> CareTeams { get; set; } = new List<CareTeam>();
public virtual ICollection<Laboratory> Laboratories { get; set; } = new List<Laboratory>();
public virtual ICollection<ImagingCenter> ImagingCenters { get; set; } = new List<ImagingCenter>();
public virtual ICollection<Pharmacy> Pharmacies { get; set; } = new List<Pharmacy>();
public virtual ICollection<InventoryItem> InventoryItems { get; set; } = new List<InventoryItem>();
 public virtual FacilityType? FacilityType { get; set; } 

    public static Facility FromRequest(FacilityRequest request) {
        return new Facility {
            Id = request.Id,
            Name = request.Name,
            FacilityCode = request.FacilityCode,
            Address = request.Address,
            FacilityType = request.FacilityType,
        };
    }
}
