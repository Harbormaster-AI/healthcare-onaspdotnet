
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Api;

public static class ProcedureEndpoints
{
    public static IEndpointRouteBuilder MapProcedureEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/procedure").WithTags("Procedures");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignEncounter", AssignEncounter);
        group.MapPut("/unassignEncounter", UnassignEncounter);
        group.MapPut("/assignPerformer", AssignPerformer);
        group.MapPut("/unassignPerformer", UnassignPerformer);
        group.MapPut("/assignProcedureOrder", AssignProcedureOrder);
        group.MapPut("/unassignProcedureOrder", UnassignProcedureOrder);


        return app;
    }

    private static async Task<IResult> Create(
        ProcedureRequest request,
        IProcedureService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToProcedure( request );

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
        ProcedureRequest request,
        IProcedureService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToProcedure( request );

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
        IProcedureService service,
        CancellationToken cancellationToken) {

        var procedure = await service.Get(identifier, cancellationToken);
        return procedure is null ? Results.NotFound() : Results.Ok( procedure );
    }


    private static async Task<IResult> GetAll(
        IProcedureService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ProcedureResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IProcedureService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignEncounter(
        AssociationRequest request,
        IProcedureService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignEncounter(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignEncounter(
    AssociationRequest request,
    IProcedureService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignEncounter(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPerformer(
        AssociationRequest request,
        IProcedureService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPerformer(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPerformer(
    AssociationRequest request,
    IProcedureService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPerformer(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignProcedureOrder(
        AssociationRequest request,
        IProcedureService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignProcedureOrder(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignProcedureOrder(
    AssociationRequest request,
    IProcedureService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignProcedureOrder(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Procedure mapRequestToProcedure( ProcedureRequest request ) {
        var model = new Procedure
        {
            Id = request.Id,
            ProcedureCode = request.ProcedureCode,
            StartDateTime = request.StartDateTime,
            EndDateTime = request.EndDateTime,
            Status = request.Status,
        };
        return model;
    }

}
