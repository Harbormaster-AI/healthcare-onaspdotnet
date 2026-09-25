
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Api;

public static class MedicalSupplierEndpoints
{
    public static IEndpointRouteBuilder MapMedicalSupplierEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/medicalSupplier").WithTags("MedicalSuppliers");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToFacilities", AddToFacilities);
    group.MapPut("/removeFromFacilities", RemoveFromFacilities);

    group.MapPut("/addToInventoryItems", AddToInventoryItems);
    group.MapPut("/removeFromInventoryItems", RemoveFromInventoryItems);


        return app;
    }

    private static async Task<IResult> Create(
        MedicalSupplierRequest request,
        IMedicalSupplierService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToMedicalSupplier( request );

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
        MedicalSupplierRequest request,
        IMedicalSupplierService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToMedicalSupplier( request );

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
        IMedicalSupplierService service,
        CancellationToken cancellationToken) {

        var medicalSupplier = await service.Get(identifier, cancellationToken);
        return medicalSupplier is null ? Results.NotFound() : Results.Ok( medicalSupplier );
    }


    private static async Task<IResult> GetAll(
        IMedicalSupplierService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( MedicalSupplierResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IMedicalSupplierService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToFacilities(
        MultipleAssociationRequest request,
        IMedicalSupplierService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToFacilities(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromFacilities(
        MultipleAssociationRequest request,
        IMedicalSupplierService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromFacilities(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToInventoryItems(
        MultipleAssociationRequest request,
        IMedicalSupplierService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToInventoryItems(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromInventoryItems(
        MultipleAssociationRequest request,
        IMedicalSupplierService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromInventoryItems(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static MedicalSupplier mapRequestToMedicalSupplier( MedicalSupplierRequest request ) {
        var model = new MedicalSupplier
        {
            Id = request.Id,
            Name = request.Name,
            Website = request.Website,
            SupplierTier = request.SupplierTier,
        };
        return model;
    }

}
