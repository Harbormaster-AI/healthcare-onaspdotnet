
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Api;

public static class DischargeEndpoints
{
    public static IEndpointRouteBuilder MapDischargeEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/discharge").WithTags("Discharges");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignEncounter", AssignEncounter);
        group.MapPut("/unassignEncounter", UnassignEncounter);


        return app;
    }

    private static async Task<IResult> Create(
        DischargeRequest request,
        IDischargeService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDischarge( request );

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
        DischargeRequest request,
        IDischargeService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDischarge( request );

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
        IDischargeService service,
        CancellationToken cancellationToken) {

        var discharge = await service.Get(identifier, cancellationToken);
        return discharge is null ? Results.NotFound() : Results.Ok( discharge );
    }


    private static async Task<IResult> GetAll(
        IDischargeService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( DischargeResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IDischargeService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignEncounter(
        AssociationRequest request,
        IDischargeService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignEncounter(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignEncounter(
    AssociationRequest request,
    IDischargeService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignEncounter(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Discharge mapRequestToDischarge( DischargeRequest request ) {
        var model = new Discharge
        {
            Id = request.Id,
            DischargeDateTime = request.DischargeDateTime,
            Disposition = request.Disposition,
        };
        return model;
    }

}
