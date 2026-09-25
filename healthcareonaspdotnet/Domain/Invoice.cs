
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class Invoice
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? InvoiceId { get; set; } 
 public virtual string? InvoiceNumber { get; set; } 
 public virtual Money? TotalAmount { get; set; } 
 public virtual DateOnly? DueDate { get; set; } 
public virtual Patient? Patient { get; set; } 
public virtual Claim? Claim { get; set; } 
public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
 public virtual InvoiceStatus? Status { get; set; } 

    public static Invoice FromRequest(InvoiceRequest request) {
        return new Invoice {
            Id = request.Id,
            InvoiceNumber = request.InvoiceNumber,
            TotalAmount = request.TotalAmount,
            DueDate = request.DueDate,
            Status = request.Status,
        };
    }
}
