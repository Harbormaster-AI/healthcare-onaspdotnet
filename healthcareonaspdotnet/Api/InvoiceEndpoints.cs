
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Api;

public static class InvoiceEndpoints
{
    public static IEndpointRouteBuilder MapInvoiceEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/invoice").WithTags("Invoices");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPatient", AssignPatient);
        group.MapPut("/unassignPatient", UnassignPatient);
        group.MapPut("/assignClaim", AssignClaim);
        group.MapPut("/unassignClaim", UnassignClaim);

    group.MapPut("/addToPayments", AddToPayments);
    group.MapPut("/removeFromPayments", RemoveFromPayments);


        return app;
    }

    private static async Task<IResult> Create(
        InvoiceRequest request,
        IInvoiceService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToInvoice( request );

        try
        {
            await service.Create(model, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }

        return Results.NoContent();
    }

    private static async Task<IResult> Update(
        InvoiceRequest request,
        IInvoiceService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToInvoice( request );

        try
        {
            var updated = await service.Update(model, cancellationToken);
            return updated ? Results.NoContent() : Results.NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }


    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        IInvoiceService service,
        CancellationToken cancellationToken) {

        var invoice = await service.Get(identifier, cancellationToken);
        return invoice is null ? Results.NotFound() : Results.Ok( invoice );
    }


    private static async Task<IResult> GetAll(
        IInvoiceService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( InvoiceResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IInvoiceService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPatient(
        AssociationRequest request,
        IInvoiceService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPatient(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPatient(
    AssociationRequest request,
    IInvoiceService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPatient(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignClaim(
        AssociationRequest request,
        IInvoiceService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignClaim(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignClaim(
    AssociationRequest request,
    IInvoiceService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignClaim(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToPayments(
        MultipleAssociationRequest request,
        IInvoiceService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPayments(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPayments(
        MultipleAssociationRequest request,
        IInvoiceService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPayments(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Invoice mapRequestToInvoice( InvoiceRequest request ) {
        var model = new Invoice
        {
            Id = request.Id,
            InvoiceNumber = request.InvoiceNumber,
            TotalAmount = request.TotalAmount,
            DueDate = request.DueDate,
            Status = request.Status,
        };
        return model;
    }

}
