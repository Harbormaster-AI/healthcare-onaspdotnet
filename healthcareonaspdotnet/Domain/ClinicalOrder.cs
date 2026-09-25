
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class ClinicalOrder
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ClinicalorderId { get; set; } 
 public virtual string? OrderNumber { get; set; } 
public virtual Patient? Patient { get; set; } 
public virtual Encounter? Encounter { get; set; } 
public virtual Clinician? OrderingClinician { get; set; } 
public virtual ICollection<MedicationOrder> MedicationOrders { get; set; } = new List<MedicationOrder>();
public virtual ICollection<LaboratoryOrder> LaboratoryOrders { get; set; } = new List<LaboratoryOrder>();
public virtual ICollection<ImagingOrder> ImagingOrders { get; set; } = new List<ImagingOrder>();
public virtual ICollection<ProcedureOrder> ProcedureOrders { get; set; } = new List<ProcedureOrder>();
public virtual ICollection<Authorization> Authorizations { get; set; } = new List<Authorization>();
 public virtual OrderStatus? Status { get; set; } 
 public virtual ClinicalOrderType? OrderType { get; set; } 
 public virtual Priority? Priority { get; set; } 

    public static ClinicalOrder FromRequest(ClinicalOrderRequest request) {
        return new ClinicalOrder {
            Id = request.Id,
            OrderNumber = request.OrderNumber,
            Status = request.Status,
            OrderType = request.OrderType,
            Priority = request.Priority,
        };
    }
}
