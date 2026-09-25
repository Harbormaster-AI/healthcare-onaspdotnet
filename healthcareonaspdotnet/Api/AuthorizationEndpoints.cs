
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Api;

public static class AuthorizationEndpoints
{
    public static IEndpointRouteBuilder MapAuthorizationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/authorization").WithTags("Authorizations");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignCoverage", AssignCoverage);
        group.MapPut("/unassignCoverage", UnassignCoverage);
        group.MapPut("/assignOrder", AssignOrder);
        group.MapPut("/unassignOrder", UnassignOrder);


        return app;
    }

    private static async Task<IResult> Create(
        AuthorizationRequest request,
        IAuthorizationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAuthorization( request );

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
        AuthorizationRequest request,
        IAuthorizationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAuthorization( request );

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
        IAuthorizationService service,
        CancellationToken cancellationToken) {

        var authorization = await service.Get(identifier, cancellationToken);
        return authorization is null ? Results.NotFound() : Results.Ok( authorization );
    }


    private static async Task<IResult> GetAll(
        IAuthorizationService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( AuthorizationResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IAuthorizationService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCoverage(
        AssociationRequest request,
        IAuthorizationService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCoverage(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCoverage(
    AssociationRequest request,
    IAuthorizationService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCoverage(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOrder(
        AssociationRequest request,
        IAuthorizationService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOrder(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOrder(
    AssociationRequest request,
    IAuthorizationService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOrder(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Authorization mapRequestToAuthorization( AuthorizationRequest request ) {
        var model = new Authorization
        {
            Id = request.Id,
            AuthNumber = request.AuthNumber,
            RequestedService = request.RequestedService,
            Status = request.Status,
        };
        return model;
    }

}
