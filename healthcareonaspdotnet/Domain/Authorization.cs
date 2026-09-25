
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class Authorization
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? AuthorizationId { get; set; } 
 public virtual string? AuthNumber { get; set; } 
 public virtual string? RequestedService { get; set; } 
public virtual Coverage? Coverage { get; set; } 
public virtual ClinicalOrder? Order { get; set; } 
 public virtual AuthorizationStatus? Status { get; set; } 

    public static Authorization FromRequest(AuthorizationRequest request) {
        return new Authorization {
            Id = request.Id,
            AuthNumber = request.AuthNumber,
            RequestedService = request.RequestedService,
            Status = request.Status,
        };
    }
}
