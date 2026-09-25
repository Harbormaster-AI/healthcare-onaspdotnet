
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Api;

public static class SoftwareUpdateEndpoints
{
    public static IEndpointRouteBuilder MapSoftwareUpdateEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/softwareUpdate").WithTags("SoftwareUpdates");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignDevice", AssignDevice);
        group.MapPut("/unassignDevice", UnassignDevice);


        return app;
    }

    private static async Task<IResult> Create(
        SoftwareUpdateRequest request,
        ISoftwareUpdateService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToSoftwareUpdate( request );

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
        SoftwareUpdateRequest request,
        ISoftwareUpdateService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToSoftwareUpdate( request );

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
        ISoftwareUpdateService service,
        CancellationToken cancellationToken) {

        var softwareUpdate = await service.Get(identifier, cancellationToken);
        return softwareUpdate is null ? Results.NotFound() : Results.Ok( softwareUpdate );
    }


    private static async Task<IResult> GetAll(
        ISoftwareUpdateService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( SoftwareUpdateResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ISoftwareUpdateService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevice(
        AssociationRequest request,
        ISoftwareUpdateService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignDevice(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignDevice(
    AssociationRequest request,
    ISoftwareUpdateService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignDevice(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static SoftwareUpdate mapRequestToSoftwareUpdate( SoftwareUpdateRequest request ) {
        var model = new SoftwareUpdate
        {
            Id = request.Id,
            Version = request.Version,
            AppliedDate = request.AppliedDate,
            UpdateType = request.UpdateType,
        };
        return model;
    }

}
