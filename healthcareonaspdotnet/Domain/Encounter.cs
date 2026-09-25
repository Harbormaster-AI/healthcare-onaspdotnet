
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class Encounter
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? EncounterId { get; set; } 
 public virtual string? EncounterNumber { get; set; } 
 public virtual DateTime? StartDateTime { get; set; } 
 public virtual DateTime? EndDateTime { get; set; } 
public virtual Patient? Patient { get; set; } 
public virtual Clinician? Clinician { get; set; } 
public virtual Facility? Facility { get; set; } 
public virtual Appointment? Appointment { get; set; } 
public virtual ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();
public virtual ICollection<Procedure> Procedures { get; set; } = new List<Procedure>();
public virtual ICollection<Observation> Observations { get; set; } = new List<Observation>();
public virtual ICollection<ClinicalOrder> Orders { get; set; } = new List<ClinicalOrder>();
public virtual Admission? Admission { get; set; } 
public virtual Discharge? Discharge { get; set; } 
 public virtual EncounterStatus? Status { get; set; } 
 public virtual EncounterType? EncounterType { get; set; } 

    public static Encounter FromRequest(EncounterRequest request) {
        return new Encounter {
            Id = request.Id,
            EncounterNumber = request.EncounterNumber,
            StartDateTime = request.StartDateTime,
            EndDateTime = request.EndDateTime,
            Status = request.Status,
            EncounterType = request.EncounterType,
        };
    }
}
