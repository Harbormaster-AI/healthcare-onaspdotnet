
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class Department
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? DepartmentId { get; set; } 
 public virtual string? Name { get; set; } 
public virtual Facility? Facility { get; set; } 
public virtual ICollection<CareTeam> CareTeams { get; set; } = new List<CareTeam>();
 public virtual DepartmentType? DepartmentType { get; set; } 

    public static Department FromRequest(DepartmentRequest request) {
        return new Department {
            Id = request.Id,
            Name = request.Name,
            DepartmentType = request.DepartmentType,
        };
    }
}
