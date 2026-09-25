
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class Patient
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PatientId { get; set; } 
 public virtual string? FirstName { get; set; } 
 public virtual string? LastName { get; set; } 
 public virtual MRN? Mrn { get; set; } 
 public virtual DateOnly? DateOfBirth { get; set; } 
 public virtual Address? Address { get; set; } 
 public virtual string? PrimaryLanguage { get; set; } 
public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
public virtual ICollection<Encounter> Encounters { get; set; } = new List<Encounter>();
public virtual ICollection<CarePlan> CarePlans { get; set; } = new List<CarePlan>();
public virtual ICollection<Allergy> Allergies { get; set; } = new List<Allergy>();
public virtual ICollection<Condition> Conditions { get; set; } = new List<Condition>();
public virtual ICollection<MedicationOrder> MedicationOrders { get; set; } = new List<MedicationOrder>();
public virtual ICollection<LaboratoryOrder> LabOrders { get; set; } = new List<LaboratoryOrder>();
public virtual ICollection<ImagingOrder> ImagingOrders { get; set; } = new List<ImagingOrder>();
public virtual ICollection<Coverage> Coverages { get; set; } = new List<Coverage>();
public virtual ICollection<Claim> Claims { get; set; } = new List<Claim>();
public virtual ICollection<MedicalDevice> Devices { get; set; } = new List<MedicalDevice>();
public virtual ICollection<Observation> Observations { get; set; } = new List<Observation>();
 public virtual AdministrativeSex? SexAtBirth { get; set; } 
 public virtual BloodType? BloodType { get; set; } 

    public static Patient FromRequest(PatientRequest request) {
        return new Patient {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Mrn = request.Mrn,
            DateOfBirth = request.DateOfBirth,
            Address = request.Address,
            PrimaryLanguage = request.PrimaryLanguage,
            SexAtBirth = request.SexAtBirth,
            BloodType = request.BloodType,
        };
    }
}
