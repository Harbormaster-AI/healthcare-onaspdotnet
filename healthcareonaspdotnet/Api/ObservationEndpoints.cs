
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Api;

public static class ObservationEndpoints
{
    public static IEndpointRouteBuilder MapObservationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/observation").WithTags("Observations");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignEncounter", AssignEncounter);
        group.MapPut("/unassignEncounter", UnassignEncounter);
        group.MapPut("/assignPatient", AssignPatient);
        group.MapPut("/unassignPatient", UnassignPatient);
        group.MapPut("/assignDevice", AssignDevice);
        group.MapPut("/unassignDevice", UnassignDevice);
        group.MapPut("/assignLabResult", AssignLabResult);
        group.MapPut("/unassignLabResult", UnassignLabResult);


        return app;
    }

    private static async Task<IResult> Create(
        ObservationRequest request,
        IObservationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToObservation( request );

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
        ObservationRequest request,
        IObservationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToObservation( request );

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
        IObservationService service,
        CancellationToken cancellationToken) {

        var observation = await service.Get(identifier, cancellationToken);
        return observation is null ? Results.NotFound() : Results.Ok( observation );
    }


    private static async Task<IResult> GetAll(
        IObservationService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ObservationResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IObservationService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignEncounter(
        AssociationRequest request,
        IObservationService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignEncounter(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignEncounter(
    AssociationRequest request,
    IObservationService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignEncounter(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPatient(
        AssociationRequest request,
        IObservationService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPatient(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPatient(
    AssociationRequest request,
    IObservationService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPatient(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevice(
        AssociationRequest request,
        IObservationService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignDevice(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignDevice(
    AssociationRequest request,
    IObservationService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignDevice(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignLabResult(
        AssociationRequest request,
        IObservationService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignLabResult(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignLabResult(
    AssociationRequest request,
    IObservationService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignLabResult(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Observation mapRequestToObservation( ObservationRequest request ) {
        var model = new Observation
        {
            Id = request.Id,
            Code = request.Code,
            Value = request.Value,
            Unit = request.Unit,
            EffectiveDateTime = request.EffectiveDateTime,
            Interpretation = request.Interpretation,
        };
        return model;
    }

}
