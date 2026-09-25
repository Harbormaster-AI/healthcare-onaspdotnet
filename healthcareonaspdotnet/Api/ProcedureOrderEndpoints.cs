
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Api;

public static class ProcedureOrderEndpoints
{
    public static IEndpointRouteBuilder MapProcedureOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/procedureOrder").WithTags("ProcedureOrders");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignOrder", AssignOrder);
        group.MapPut("/unassignOrder", UnassignOrder);
        group.MapPut("/assignFacility", AssignFacility);
        group.MapPut("/unassignFacility", UnassignFacility);
        group.MapPut("/assignProcedure", AssignProcedure);
        group.MapPut("/unassignProcedure", UnassignProcedure);


        return app;
    }

    private static async Task<IResult> Create(
        ProcedureOrderRequest request,
        IProcedureOrderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToProcedureOrder( request );

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
        ProcedureOrderRequest request,
        IProcedureOrderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToProcedureOrder( request );

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
        IProcedureOrderService service,
        CancellationToken cancellationToken) {

        var procedureOrder = await service.Get(identifier, cancellationToken);
        return procedureOrder is null ? Results.NotFound() : Results.Ok( procedureOrder );
    }


    private static async Task<IResult> GetAll(
        IProcedureOrderService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ProcedureOrderResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IProcedureOrderService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOrder(
        AssociationRequest request,
        IProcedureOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOrder(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOrder(
    AssociationRequest request,
    IProcedureOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOrder(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignFacility(
        AssociationRequest request,
        IProcedureOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignFacility(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignFacility(
    AssociationRequest request,
    IProcedureOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignFacility(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignProcedure(
        AssociationRequest request,
        IProcedureOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignProcedure(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignProcedure(
    AssociationRequest request,
    IProcedureOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignProcedure(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static ProcedureOrder mapRequestToProcedureOrder( ProcedureOrderRequest request ) {
        var model = new ProcedureOrder
        {
            Id = request.Id,
            ProcedureCode = request.ProcedureCode,
            ConsentObtained = request.ConsentObtained,
            AnesthesiaType = request.AnesthesiaType,
        };
        return model;
    }

}
