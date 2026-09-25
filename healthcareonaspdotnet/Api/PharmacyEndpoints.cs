
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Api;

public static class PharmacyEndpoints
{
    public static IEndpointRouteBuilder MapPharmacyEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/pharmacy").WithTags("Pharmacys");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignFacility", AssignFacility);
        group.MapPut("/unassignFacility", UnassignFacility);

    group.MapPut("/addToMedicationDispenses", AddToMedicationDispenses);
    group.MapPut("/removeFromMedicationDispenses", RemoveFromMedicationDispenses);

    group.MapPut("/addToMedicationOrders", AddToMedicationOrders);
    group.MapPut("/removeFromMedicationOrders", RemoveFromMedicationOrders);


        return app;
    }

    private static async Task<IResult> Create(
        PharmacyRequest request,
        IPharmacyService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPharmacy( request );

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
        PharmacyRequest request,
        IPharmacyService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPharmacy( request );

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
        IPharmacyService service,
        CancellationToken cancellationToken) {

        var pharmacy = await service.Get(identifier, cancellationToken);
        return pharmacy is null ? Results.NotFound() : Results.Ok( pharmacy );
    }


    private static async Task<IResult> GetAll(
        IPharmacyService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( PharmacyResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IPharmacyService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignFacility(
        AssociationRequest request,
        IPharmacyService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignFacility(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignFacility(
    AssociationRequest request,
    IPharmacyService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignFacility(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToMedicationDispenses(
        MultipleAssociationRequest request,
        IPharmacyService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToMedicationDispenses(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromMedicationDispenses(
        MultipleAssociationRequest request,
        IPharmacyService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromMedicationDispenses(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToMedicationOrders(
        MultipleAssociationRequest request,
        IPharmacyService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToMedicationOrders(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromMedicationOrders(
        MultipleAssociationRequest request,
        IPharmacyService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromMedicationOrders(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Pharmacy mapRequestToPharmacy( PharmacyRequest request ) {
        var model = new Pharmacy
        {
            Id = request.Id,
            Name = request.Name,
        };
        return model;
    }

}
