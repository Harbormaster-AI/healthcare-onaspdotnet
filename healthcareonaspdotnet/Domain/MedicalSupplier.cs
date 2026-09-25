
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class MedicalSupplier
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? MedicalsupplierId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Website { get; set; } 
public virtual ICollection<Facility> Facilities { get; set; } = new List<Facility>();
public virtual ICollection<InventoryItem> InventoryItems { get; set; } = new List<InventoryItem>();
 public virtual SupplierTier? SupplierTier { get; set; } 

    public static MedicalSupplier FromRequest(MedicalSupplierRequest request) {
        return new MedicalSupplier {
            Id = request.Id,
            Name = request.Name,
            Website = request.Website,
            SupplierTier = request.SupplierTier,
        };
    }
}
