
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Api;

public static class CareTeamEndpoints
{
    public static IEndpointRouteBuilder MapCareTeamEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/careTeam").WithTags("CareTeams");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignDepartment", AssignDepartment);
        group.MapPut("/unassignDepartment", UnassignDepartment);

    group.MapPut("/addToClinicians", AddToClinicians);
    group.MapPut("/removeFromClinicians", RemoveFromClinicians);

    group.MapPut("/addToPatients", AddToPatients);
    group.MapPut("/removeFromPatients", RemoveFromPatients);


        return app;
    }

    private static async Task<IResult> Create(
        CareTeamRequest request,
        ICareTeamService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCareTeam( request );

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
        CareTeamRequest request,
        ICareTeamService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCareTeam( request );

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
        ICareTeamService service,
        CancellationToken cancellationToken) {

        var careTeam = await service.Get(identifier, cancellationToken);
        return careTeam is null ? Results.NotFound() : Results.Ok( careTeam );
    }


    private static async Task<IResult> GetAll(
        ICareTeamService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( CareTeamResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ICareTeamService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDepartment(
        AssociationRequest request,
        ICareTeamService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignDepartment(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignDepartment(
    AssociationRequest request,
    ICareTeamService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignDepartment(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToClinicians(
        MultipleAssociationRequest request,
        ICareTeamService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToClinicians(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromClinicians(
        MultipleAssociationRequest request,
        ICareTeamService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromClinicians(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToPatients(
        MultipleAssociationRequest request,
        ICareTeamService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPatients(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPatients(
        MultipleAssociationRequest request,
        ICareTeamService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPatients(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static CareTeam mapRequestToCareTeam( CareTeamRequest request ) {
        var model = new CareTeam
        {
            Id = request.Id,
            Name = request.Name,
            CareSetting = request.CareSetting,
        };
        return model;
    }

}
