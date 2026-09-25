using System.ComponentModel.DataAnnotations.Schema;

namespace healthcareonaspdotnet.Domain;


    [ComplexType]
    public record MRN(
    string Value
    );

    [ComplexType]
    public record Money(
    decimal Amount,
    string Currency
    );

    [ComplexType]
    public record Address(
    string Street,
    string City,
    string State,
    string PostalCode,
    string Country
    );

    [ComplexType]
    public record Dose(
    decimal Amount,
    string Unit
    );

