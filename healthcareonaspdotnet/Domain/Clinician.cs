
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class Clinician
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ClinicianId { get; set; } 
 public virtual string? FirstName { get; set; } 
 public virtual string? LastName { get; set; } 
 public virtual string? LicenseNumber { get; set; } 
public virtual ICollection<CareTeam> CareTeams { get; set; } = new List<CareTeam>();
public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
public virtual ICollection<Encounter> Encounters { get; set; } = new List<Encounter>();
public virtual ICollection<Procedure> Procedures { get; set; } = new List<Procedure>();
public virtual ICollection<ImagingReport> ImagingReports { get; set; } = new List<ImagingReport>();
 public virtual ClinicianType? ClinicianType { get; set; } 
 public virtual ClinicianSpecialty? Specialty { get; set; } 

    public static Clinician FromRequest(ClinicianRequest request) {
        return new Clinician {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            LicenseNumber = request.LicenseNumber,
            ClinicianType = request.ClinicianType,
            Specialty = request.Specialty,
        };
    }
}
