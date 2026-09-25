
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Domain;

public class Payment
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PaymentId { get; set; } 
 public virtual string? PaymentNumber { get; set; } 
 public virtual Money? Amount { get; set; } 
 public virtual DateOnly? PaymentDate { get; set; } 
public virtual Invoice? Invoice { get; set; } 
public virtual InsurancePayer? Payer { get; set; } 
 public virtual PaymentMethod? Method { get; set; } 

    public static Payment FromRequest(PaymentRequest request) {
        return new Payment {
            Id = request.Id,
            PaymentNumber = request.PaymentNumber,
            Amount = request.Amount,
            PaymentDate = request.PaymentDate,
            Method = request.Method,
        };
    }
}
