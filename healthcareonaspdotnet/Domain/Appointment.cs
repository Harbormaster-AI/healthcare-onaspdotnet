
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class Appointment
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? AppointmentId { get; set; } 
 public virtual DateTime? AppointmentDate { get; set; } 
 public virtual string? Reason { get; set; } 
public virtual Patient? Patient { get; set; } 
public virtual Clinician? Clinician { get; set; } 
public virtual Facility? Facility { get; set; } 
public virtual Encounter? Encounter { get; set; } 
 public virtual AppointmentStatus? Status { get; set; } 
 public virtual Priority? Priority { get; set; } 

    public static Appointment FromRequest(AppointmentRequest request) {
        return new Appointment {
            Id = request.Id,
            AppointmentDate = request.AppointmentDate,
            Reason = request.Reason,
            Status = request.Status,
            Priority = request.Priority,
        };
    }
}
