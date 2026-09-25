
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Api;

public static class ImagingOrderEndpoints
{
    public static IEndpointRouteBuilder MapImagingOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/imagingOrder").WithTags("ImagingOrders");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignOrder", AssignOrder);
        group.MapPut("/unassignOrder", UnassignOrder);
        group.MapPut("/assignImagingCenter", AssignImagingCenter);
        group.MapPut("/unassignImagingCenter", UnassignImagingCenter);

    group.MapPut("/addToReports", AddToReports);
    group.MapPut("/removeFromReports", RemoveFromReports);


        return app;
    }

    private static async Task<IResult> Create(
        ImagingOrderRequest request,
        IImagingOrderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToImagingOrder( request );

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
        ImagingOrderRequest request,
        IImagingOrderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToImagingOrder( request );

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
        IImagingOrderService service,
        CancellationToken cancellationToken) {

        var imagingOrder = await service.Get(identifier, cancellationToken);
        return imagingOrder is null ? Results.NotFound() : Results.Ok( imagingOrder );
    }


    private static async Task<IResult> GetAll(
        IImagingOrderService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ImagingOrderResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IImagingOrderService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOrder(
        AssociationRequest request,
        IImagingOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOrder(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOrder(
    AssociationRequest request,
    IImagingOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOrder(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignImagingCenter(
        AssociationRequest request,
        IImagingOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignImagingCenter(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignImagingCenter(
    AssociationRequest request,
    IImagingOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignImagingCenter(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToReports(
        MultipleAssociationRequest request,
        IImagingOrderService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToReports(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromReports(
        MultipleAssociationRequest request,
        IImagingOrderService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromReports(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static ImagingOrder mapRequestToImagingOrder( ImagingOrderRequest request ) {
        var model = new ImagingOrder
        {
            Id = request.Id,
            BodySite = request.BodySite,
            Contrast = request.Contrast,
            Modality = request.Modality,
        };
        return model;
    }

}
