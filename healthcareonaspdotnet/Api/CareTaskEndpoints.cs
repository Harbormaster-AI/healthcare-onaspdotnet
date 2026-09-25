
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Api;

public static class CareTaskEndpoints
{
    public static IEndpointRouteBuilder MapCareTaskEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/careTask").WithTags("CareTasks");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignCarePlan", AssignCarePlan);
        group.MapPut("/unassignCarePlan", UnassignCarePlan);
        group.MapPut("/assignAssignedTo", AssignAssignedTo);
        group.MapPut("/unassignAssignedTo", UnassignAssignedTo);
        group.MapPut("/assignEncounter", AssignEncounter);
        group.MapPut("/unassignEncounter", UnassignEncounter);


        return app;
    }

    private static async Task<IResult> Create(
        CareTaskRequest request,
        ICareTaskService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCareTask( request );

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
        CareTaskRequest request,
        ICareTaskService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCareTask( request );

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
        ICareTaskService service,
        CancellationToken cancellationToken) {

        var careTask = await service.Get(identifier, cancellationToken);
        return careTask is null ? Results.NotFound() : Results.Ok( careTask );
    }


    private static async Task<IResult> GetAll(
        ICareTaskService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( CareTaskResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ICareTaskService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCarePlan(
        AssociationRequest request,
        ICareTaskService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCarePlan(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCarePlan(
    AssociationRequest request,
    ICareTaskService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCarePlan(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAssignedTo(
        AssociationRequest request,
        ICareTaskService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAssignedTo(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAssignedTo(
    AssociationRequest request,
    ICareTaskService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAssignedTo(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignEncounter(
        AssociationRequest request,
        ICareTaskService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignEncounter(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignEncounter(
    AssociationRequest request,
    ICareTaskService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignEncounter(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static CareTask mapRequestToCareTask( CareTaskRequest request ) {
        var model = new CareTask
        {
            Id = request.Id,
            Description = request.Description,
            DueDate = request.DueDate,
            Status = request.Status,
            Priority = request.Priority,
        };
        return model;
    }

}
