
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Api;

public static class LaboratoryOrderEndpoints
{
    public static IEndpointRouteBuilder MapLaboratoryOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/laboratoryOrder").WithTags("LaboratoryOrders");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignOrder", AssignOrder);
        group.MapPut("/unassignOrder", UnassignOrder);
        group.MapPut("/assignLaboratory", AssignLaboratory);
        group.MapPut("/unassignLaboratory", UnassignLaboratory);

    group.MapPut("/addToResults", AddToResults);
    group.MapPut("/removeFromResults", RemoveFromResults);


        return app;
    }

    private static async Task<IResult> Create(
        LaboratoryOrderRequest request,
        ILaboratoryOrderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToLaboratoryOrder( request );

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
        LaboratoryOrderRequest request,
        ILaboratoryOrderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToLaboratoryOrder( request );

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
        ILaboratoryOrderService service,
        CancellationToken cancellationToken) {

        var laboratoryOrder = await service.Get(identifier, cancellationToken);
        return laboratoryOrder is null ? Results.NotFound() : Results.Ok( laboratoryOrder );
    }


    private static async Task<IResult> GetAll(
        ILaboratoryOrderService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( LaboratoryOrderResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ILaboratoryOrderService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOrder(
        AssociationRequest request,
        ILaboratoryOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOrder(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOrder(
    AssociationRequest request,
    ILaboratoryOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOrder(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignLaboratory(
        AssociationRequest request,
        ILaboratoryOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignLaboratory(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignLaboratory(
    AssociationRequest request,
    ILaboratoryOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignLaboratory(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToResults(
        MultipleAssociationRequest request,
        ILaboratoryOrderService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToResults(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromResults(
        MultipleAssociationRequest request,
        ILaboratoryOrderService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromResults(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static LaboratoryOrder mapRequestToLaboratoryOrder( LaboratoryOrderRequest request ) {
        var model = new LaboratoryOrder
        {
            Id = request.Id,
            TestCode = request.TestCode,
            FastingRequired = request.FastingRequired,
            SpecimenType = request.SpecimenType,
        };
        return model;
    }

}
