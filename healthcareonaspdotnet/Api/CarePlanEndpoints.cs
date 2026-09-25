
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Api;

public static class CarePlanEndpoints
{
    public static IEndpointRouteBuilder MapCarePlanEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/carePlan").WithTags("CarePlans");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPatient", AssignPatient);
        group.MapPut("/unassignPatient", UnassignPatient);
        group.MapPut("/assignCareTeam", AssignCareTeam);
        group.MapPut("/unassignCareTeam", UnassignCareTeam);

    group.MapPut("/addToEncounters", AddToEncounters);
    group.MapPut("/removeFromEncounters", RemoveFromEncounters);

    group.MapPut("/addToTasks", AddToTasks);
    group.MapPut("/removeFromTasks", RemoveFromTasks);


        return app;
    }

    private static async Task<IResult> Create(
        CarePlanRequest request,
        ICarePlanService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCarePlan( request );

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
        CarePlanRequest request,
        ICarePlanService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCarePlan( request );

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
        ICarePlanService service,
        CancellationToken cancellationToken) {

        var carePlan = await service.Get(identifier, cancellationToken);
        return carePlan is null ? Results.NotFound() : Results.Ok( carePlan );
    }


    private static async Task<IResult> GetAll(
        ICarePlanService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( CarePlanResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ICarePlanService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPatient(
        AssociationRequest request,
        ICarePlanService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPatient(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPatient(
    AssociationRequest request,
    ICarePlanService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPatient(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCareTeam(
        AssociationRequest request,
        ICarePlanService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCareTeam(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCareTeam(
    AssociationRequest request,
    ICarePlanService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCareTeam(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToEncounters(
        MultipleAssociationRequest request,
        ICarePlanService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToEncounters(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromEncounters(
        MultipleAssociationRequest request,
        ICarePlanService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromEncounters(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToTasks(
        MultipleAssociationRequest request,
        ICarePlanService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToTasks(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromTasks(
        MultipleAssociationRequest request,
        ICarePlanService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromTasks(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static CarePlan mapRequestToCarePlan( CarePlanRequest request ) {
        var model = new CarePlan
        {
            Id = request.Id,
            PlanNumber = request.PlanNumber,
            GoalSummary = request.GoalSummary,
            Status = request.Status,
        };
        return model;
    }

}
