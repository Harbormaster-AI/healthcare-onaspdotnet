
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Api;

public static class CoverageEndpoints
{
    public static IEndpointRouteBuilder MapCoverageEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/coverage").WithTags("Coverages");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPatient", AssignPatient);
        group.MapPut("/unassignPatient", UnassignPatient);
        group.MapPut("/assignPlan", AssignPlan);
        group.MapPut("/unassignPlan", UnassignPlan);

    group.MapPut("/addToClaims", AddToClaims);
    group.MapPut("/removeFromClaims", RemoveFromClaims);

    group.MapPut("/addToAuthorizations", AddToAuthorizations);
    group.MapPut("/removeFromAuthorizations", RemoveFromAuthorizations);


        return app;
    }

    private static async Task<IResult> Create(
        CoverageRequest request,
        ICoverageService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCoverage( request );

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
        CoverageRequest request,
        ICoverageService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCoverage( request );

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
        ICoverageService service,
        CancellationToken cancellationToken) {

        var coverage = await service.Get(identifier, cancellationToken);
        return coverage is null ? Results.NotFound() : Results.Ok( coverage );
    }


    private static async Task<IResult> GetAll(
        ICoverageService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( CoverageResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ICoverageService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPatient(
        AssociationRequest request,
        ICoverageService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPatient(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPatient(
    AssociationRequest request,
    ICoverageService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPatient(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPlan(
        AssociationRequest request,
        ICoverageService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPlan(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPlan(
    AssociationRequest request,
    ICoverageService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPlan(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToClaims(
        MultipleAssociationRequest request,
        ICoverageService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToClaims(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromClaims(
        MultipleAssociationRequest request,
        ICoverageService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromClaims(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToAuthorizations(
        MultipleAssociationRequest request,
        ICoverageService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAuthorizations(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAuthorizations(
        MultipleAssociationRequest request,
        ICoverageService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAuthorizations(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Coverage mapRequestToCoverage( CoverageRequest request ) {
        var model = new Coverage
        {
            Id = request.Id,
            MemberId = request.MemberId,
            GroupNumber = request.GroupNumber,
            EffectiveDate = request.EffectiveDate,
            EndDate = request.EndDate,
            CoverageType = request.CoverageType,
        };
        return model;
    }

}
