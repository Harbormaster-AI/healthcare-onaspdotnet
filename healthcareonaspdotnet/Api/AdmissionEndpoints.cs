
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Api;

public static class AdmissionEndpoints
{
    public static IEndpointRouteBuilder MapAdmissionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/admission").WithTags("Admissions");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignEncounter", AssignEncounter);
        group.MapPut("/unassignEncounter", UnassignEncounter);
        group.MapPut("/assignFacility", AssignFacility);
        group.MapPut("/unassignFacility", UnassignFacility);


        return app;
    }

    private static async Task<IResult> Create(
        AdmissionRequest request,
        IAdmissionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAdmission( request );

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
        AdmissionRequest request,
        IAdmissionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAdmission( request );

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
        IAdmissionService service,
        CancellationToken cancellationToken) {

        var admission = await service.Get(identifier, cancellationToken);
        return admission is null ? Results.NotFound() : Results.Ok( admission );
    }


    private static async Task<IResult> GetAll(
        IAdmissionService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( AdmissionResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IAdmissionService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignEncounter(
        AssociationRequest request,
        IAdmissionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignEncounter(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignEncounter(
    AssociationRequest request,
    IAdmissionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignEncounter(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignFacility(
        AssociationRequest request,
        IAdmissionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignFacility(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignFacility(
    AssociationRequest request,
    IAdmissionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignFacility(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Admission mapRequestToAdmission( AdmissionRequest request ) {
        var model = new Admission
        {
            Id = request.Id,
            AdmitDateTime = request.AdmitDateTime,
            Bed = request.Bed,
            AdmissionType = request.AdmissionType,
        };
        return model;
    }

}
