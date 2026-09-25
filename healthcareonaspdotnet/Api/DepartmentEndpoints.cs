
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Api;

public static class DepartmentEndpoints
{
    public static IEndpointRouteBuilder MapDepartmentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/department").WithTags("Departments");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignFacility", AssignFacility);
        group.MapPut("/unassignFacility", UnassignFacility);

    group.MapPut("/addToCareTeams", AddToCareTeams);
    group.MapPut("/removeFromCareTeams", RemoveFromCareTeams);


        return app;
    }

    private static async Task<IResult> Create(
        DepartmentRequest request,
        IDepartmentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDepartment( request );

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
        DepartmentRequest request,
        IDepartmentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDepartment( request );

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
        IDepartmentService service,
        CancellationToken cancellationToken) {

        var department = await service.Get(identifier, cancellationToken);
        return department is null ? Results.NotFound() : Results.Ok( department );
    }


    private static async Task<IResult> GetAll(
        IDepartmentService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( DepartmentResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IDepartmentService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignFacility(
        AssociationRequest request,
        IDepartmentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignFacility(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignFacility(
    AssociationRequest request,
    IDepartmentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignFacility(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToCareTeams(
        MultipleAssociationRequest request,
        IDepartmentService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCareTeams(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCareTeams(
        MultipleAssociationRequest request,
        IDepartmentService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCareTeams(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Department mapRequestToDepartment( DepartmentRequest request ) {
        var model = new Department
        {
            Id = request.Id,
            Name = request.Name,
            DepartmentType = request.DepartmentType,
        };
        return model;
    }

}
