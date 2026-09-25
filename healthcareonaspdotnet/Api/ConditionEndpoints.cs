
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Api;

public static class ConditionEndpoints
{
    public static IEndpointRouteBuilder MapConditionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/condition").WithTags("Conditions");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPatient", AssignPatient);
        group.MapPut("/unassignPatient", UnassignPatient);


        return app;
    }

    private static async Task<IResult> Create(
        ConditionRequest request,
        IConditionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCondition( request );

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
        ConditionRequest request,
        IConditionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCondition( request );

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
        IConditionService service,
        CancellationToken cancellationToken) {

        var condition = await service.Get(identifier, cancellationToken);
        return condition is null ? Results.NotFound() : Results.Ok( condition );
    }


    private static async Task<IResult> GetAll(
        IConditionService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ConditionResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IConditionService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPatient(
        AssociationRequest request,
        IConditionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPatient(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPatient(
    AssociationRequest request,
    IConditionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPatient(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Condition mapRequestToCondition( ConditionRequest request ) {
        var model = new Condition
        {
            Id = request.Id,
            Code = request.Code,
            OnsetDate = request.OnsetDate,
            AbatementDate = request.AbatementDate,
            ClinicalStatus = request.ClinicalStatus,
            VerificationStatus = request.VerificationStatus,
        };
        return model;
    }

}
