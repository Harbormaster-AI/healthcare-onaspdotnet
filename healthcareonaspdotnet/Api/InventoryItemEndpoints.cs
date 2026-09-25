
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Api;

public static class InventoryItemEndpoints
{
    public static IEndpointRouteBuilder MapInventoryItemEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/inventoryItem").WithTags("InventoryItems");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignFacility", AssignFacility);
        group.MapPut("/unassignFacility", UnassignFacility);
        group.MapPut("/assignSupplier", AssignSupplier);
        group.MapPut("/unassignSupplier", UnassignSupplier);


        return app;
    }

    private static async Task<IResult> Create(
        InventoryItemRequest request,
        IInventoryItemService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToInventoryItem( request );

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
        InventoryItemRequest request,
        IInventoryItemService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToInventoryItem( request );

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
        IInventoryItemService service,
        CancellationToken cancellationToken) {

        var inventoryItem = await service.Get(identifier, cancellationToken);
        return inventoryItem is null ? Results.NotFound() : Results.Ok( inventoryItem );
    }


    private static async Task<IResult> GetAll(
        IInventoryItemService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( InventoryItemResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IInventoryItemService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignFacility(
        AssociationRequest request,
        IInventoryItemService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignFacility(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignFacility(
    AssociationRequest request,
    IInventoryItemService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignFacility(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSupplier(
        AssociationRequest request,
        IInventoryItemService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignSupplier(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignSupplier(
    AssociationRequest request,
    IInventoryItemService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignSupplier(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static InventoryItem mapRequestToInventoryItem( InventoryItemRequest request ) {
        var model = new InventoryItem
        {
            Id = request.Id,
            Sku = request.Sku,
            Name = request.Name,
            QuantityOnHand = request.QuantityOnHand,
            QuantityReserved = request.QuantityReserved,
        };
        return model;
    }

}
