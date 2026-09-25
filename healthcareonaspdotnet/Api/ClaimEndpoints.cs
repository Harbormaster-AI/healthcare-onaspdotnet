
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Api;

public static class ClaimEndpoints
{
    public static IEndpointRouteBuilder MapClaimEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/claim").WithTags("Claims");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPatient", AssignPatient);
        group.MapPut("/unassignPatient", UnassignPatient);
        group.MapPut("/assignCoverage", AssignCoverage);
        group.MapPut("/unassignCoverage", UnassignCoverage);
        group.MapPut("/assignEncounter", AssignEncounter);
        group.MapPut("/unassignEncounter", UnassignEncounter);
        group.MapPut("/assignPayer", AssignPayer);
        group.MapPut("/unassignPayer", UnassignPayer);

    group.MapPut("/addToInvoices", AddToInvoices);
    group.MapPut("/removeFromInvoices", RemoveFromInvoices);


        return app;
    }

    private static async Task<IResult> Create(
        ClaimRequest request,
        IClaimService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToClaim( request );

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
        ClaimRequest request,
        IClaimService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToClaim( request );

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
        IClaimService service,
        CancellationToken cancellationToken) {

        var claim = await service.Get(identifier, cancellationToken);
        return claim is null ? Results.NotFound() : Results.Ok( claim );
    }


    private static async Task<IResult> GetAll(
        IClaimService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ClaimResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IClaimService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPatient(
        AssociationRequest request,
        IClaimService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPatient(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPatient(
    AssociationRequest request,
    IClaimService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPatient(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCoverage(
        AssociationRequest request,
        IClaimService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCoverage(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCoverage(
    AssociationRequest request,
    IClaimService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCoverage(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignEncounter(
        AssociationRequest request,
        IClaimService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignEncounter(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignEncounter(
    AssociationRequest request,
    IClaimService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignEncounter(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPayer(
        AssociationRequest request,
        IClaimService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPayer(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPayer(
    AssociationRequest request,
    IClaimService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPayer(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToInvoices(
        MultipleAssociationRequest request,
        IClaimService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToInvoices(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromInvoices(
        MultipleAssociationRequest request,
        IClaimService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromInvoices(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Claim mapRequestToClaim( ClaimRequest request ) {
        var model = new Claim
        {
            Id = request.Id,
            ClaimNumber = request.ClaimNumber,
            TotalAmount = request.TotalAmount,
            Status = request.Status,
        };
        return model;
    }

}
