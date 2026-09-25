
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Api;

public static class LabResultEndpoints
{
    public static IEndpointRouteBuilder MapLabResultEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/labResult").WithTags("LabResults");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignLaboratoryOrder", AssignLaboratoryOrder);
        group.MapPut("/unassignLaboratoryOrder", UnassignLaboratoryOrder);
        group.MapPut("/assignLaboratory", AssignLaboratory);
        group.MapPut("/unassignLaboratory", UnassignLaboratory);

    group.MapPut("/addToObservations", AddToObservations);
    group.MapPut("/removeFromObservations", RemoveFromObservations);


        return app;
    }

    private static async Task<IResult> Create(
        LabResultRequest request,
        ILabResultService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToLabResult( request );

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
        LabResultRequest request,
        ILabResultService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToLabResult( request );

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
        ILabResultService service,
        CancellationToken cancellationToken) {

        var labResult = await service.Get(identifier, cancellationToken);
        return labResult is null ? Results.NotFound() : Results.Ok( labResult );
    }


    private static async Task<IResult> GetAll(
        ILabResultService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( LabResultResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ILabResultService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignLaboratoryOrder(
        AssociationRequest request,
        ILabResultService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignLaboratoryOrder(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignLaboratoryOrder(
    AssociationRequest request,
    ILabResultService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignLaboratoryOrder(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignLaboratory(
        AssociationRequest request,
        ILabResultService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignLaboratory(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignLaboratory(
    AssociationRequest request,
    ILabResultService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignLaboratory(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToObservations(
        MultipleAssociationRequest request,
        ILabResultService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToObservations(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromObservations(
        MultipleAssociationRequest request,
        ILabResultService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromObservations(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static LabResult mapRequestToLabResult( LabResultRequest request ) {
        var model = new LabResult
        {
            Id = request.Id,
            ResultCode = request.ResultCode,
            IssuedDate = request.IssuedDate,
            Status = request.Status,
        };
        return model;
    }

}
