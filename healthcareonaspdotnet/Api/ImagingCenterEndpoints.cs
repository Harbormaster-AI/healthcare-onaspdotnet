
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Api;

public static class ImagingCenterEndpoints
{
    public static IEndpointRouteBuilder MapImagingCenterEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/imagingCenter").WithTags("ImagingCenters");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignFacility", AssignFacility);
        group.MapPut("/unassignFacility", UnassignFacility);

    group.MapPut("/addToImagingOrders", AddToImagingOrders);
    group.MapPut("/removeFromImagingOrders", RemoveFromImagingOrders);

    group.MapPut("/addToImagingReports", AddToImagingReports);
    group.MapPut("/removeFromImagingReports", RemoveFromImagingReports);


        return app;
    }

    private static async Task<IResult> Create(
        ImagingCenterRequest request,
        IImagingCenterService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToImagingCenter( request );

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
        ImagingCenterRequest request,
        IImagingCenterService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToImagingCenter( request );

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
        IImagingCenterService service,
        CancellationToken cancellationToken) {

        var imagingCenter = await service.Get(identifier, cancellationToken);
        return imagingCenter is null ? Results.NotFound() : Results.Ok( imagingCenter );
    }


    private static async Task<IResult> GetAll(
        IImagingCenterService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ImagingCenterResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IImagingCenterService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignFacility(
        AssociationRequest request,
        IImagingCenterService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignFacility(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignFacility(
    AssociationRequest request,
    IImagingCenterService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignFacility(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToImagingOrders(
        MultipleAssociationRequest request,
        IImagingCenterService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToImagingOrders(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromImagingOrders(
        MultipleAssociationRequest request,
        IImagingCenterService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromImagingOrders(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToImagingReports(
        MultipleAssociationRequest request,
        IImagingCenterService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToImagingReports(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromImagingReports(
        MultipleAssociationRequest request,
        IImagingCenterService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromImagingReports(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static ImagingCenter mapRequestToImagingCenter( ImagingCenterRequest request ) {
        var model = new ImagingCenter
        {
            Id = request.Id,
            Name = request.Name,
        };
        return model;
    }

}
