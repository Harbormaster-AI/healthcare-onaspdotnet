
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class InventoryItem
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? InventoryitemId { get; set; } 
 public virtual string? Sku { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual int? QuantityOnHand { get; set; } 
 public virtual int? QuantityReserved { get; set; } 
public virtual Facility? Facility { get; set; } 
public virtual MedicalSupplier? Supplier { get; set; } 

    public static InventoryItem FromRequest(InventoryItemRequest request) {
        return new InventoryItem {
            Id = request.Id,
            Sku = request.Sku,
            Name = request.Name,
            QuantityOnHand = request.QuantityOnHand,
            QuantityReserved = request.QuantityReserved,
        };
    }
}
