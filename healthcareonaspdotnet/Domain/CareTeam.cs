
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class CareTeam
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? CareteamId { get; set; } 
 public virtual string? Name { get; set; } 
public virtual Department? Department { get; set; } 
public virtual ICollection<Clinician> Clinicians { get; set; } = new List<Clinician>();
public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
 public virtual CareSettingType? CareSetting { get; set; } 

    public static CareTeam FromRequest(CareTeamRequest request) {
        return new CareTeam {
            Id = request.Id,
            Name = request.Name,
            CareSetting = request.CareSetting,
        };
    }
}
