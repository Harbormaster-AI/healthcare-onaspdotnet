
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Api;

public static class LaboratoryEndpoints
{
    public static IEndpointRouteBuilder MapLaboratoryEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/laboratory").WithTags("Laboratorys");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignFacility", AssignFacility);
        group.MapPut("/unassignFacility", UnassignFacility);

    group.MapPut("/addToLaboratoryOrders", AddToLaboratoryOrders);
    group.MapPut("/removeFromLaboratoryOrders", RemoveFromLaboratoryOrders);

    group.MapPut("/addToLabResults", AddToLabResults);
    group.MapPut("/removeFromLabResults", RemoveFromLabResults);


        return app;
    }

    private static async Task<IResult> Create(
        LaboratoryRequest request,
        ILaboratoryService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToLaboratory( request );

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
        LaboratoryRequest request,
        ILaboratoryService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToLaboratory( request );

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
        ILaboratoryService service,
        CancellationToken cancellationToken) {

        var laboratory = await service.Get(identifier, cancellationToken);
        return laboratory is null ? Results.NotFound() : Results.Ok( laboratory );
    }


    private static async Task<IResult> GetAll(
        ILaboratoryService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( LaboratoryResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ILaboratoryService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignFacility(
        AssociationRequest request,
        ILaboratoryService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignFacility(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignFacility(
    AssociationRequest request,
    ILaboratoryService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignFacility(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToLaboratoryOrders(
        MultipleAssociationRequest request,
        ILaboratoryService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToLaboratoryOrders(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromLaboratoryOrders(
        MultipleAssociationRequest request,
        ILaboratoryService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromLaboratoryOrders(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToLabResults(
        MultipleAssociationRequest request,
        ILaboratoryService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToLabResults(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromLabResults(
        MultipleAssociationRequest request,
        ILaboratoryService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromLabResults(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Laboratory mapRequestToLaboratory( LaboratoryRequest request ) {
        var model = new Laboratory
        {
            Id = request.Id,
            Name = request.Name,
            CliaNumber = request.CliaNumber,
        };
        return model;
    }

}
