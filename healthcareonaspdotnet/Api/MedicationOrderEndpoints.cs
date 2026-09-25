
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Api;

public static class MedicationOrderEndpoints
{
    public static IEndpointRouteBuilder MapMedicationOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/medicationOrder").WithTags("MedicationOrders");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignOrder", AssignOrder);
        group.MapPut("/unassignOrder", UnassignOrder);
        group.MapPut("/assignPharmacy", AssignPharmacy);
        group.MapPut("/unassignPharmacy", UnassignPharmacy);

    group.MapPut("/addToDispenses", AddToDispenses);
    group.MapPut("/removeFromDispenses", RemoveFromDispenses);


        return app;
    }

    private static async Task<IResult> Create(
        MedicationOrderRequest request,
        IMedicationOrderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToMedicationOrder( request );

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
        MedicationOrderRequest request,
        IMedicationOrderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToMedicationOrder( request );

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
        IMedicationOrderService service,
        CancellationToken cancellationToken) {

        var medicationOrder = await service.Get(identifier, cancellationToken);
        return medicationOrder is null ? Results.NotFound() : Results.Ok( medicationOrder );
    }


    private static async Task<IResult> GetAll(
        IMedicationOrderService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( MedicationOrderResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IMedicationOrderService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOrder(
        AssociationRequest request,
        IMedicationOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOrder(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOrder(
    AssociationRequest request,
    IMedicationOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOrder(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPharmacy(
        AssociationRequest request,
        IMedicationOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPharmacy(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPharmacy(
    AssociationRequest request,
    IMedicationOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPharmacy(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToDispenses(
        MultipleAssociationRequest request,
        IMedicationOrderService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDispenses(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDispenses(
        MultipleAssociationRequest request,
        IMedicationOrderService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDispenses(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static MedicationOrder mapRequestToMedicationOrder( MedicationOrderRequest request ) {
        var model = new MedicationOrder
        {
            Id = request.Id,
            MedicationCode = request.MedicationCode,
            Dose = request.Dose,
            Frequency = request.Frequency,
            Duration = request.Duration,
            Route = request.Route,
        };
        return model;
    }

}
