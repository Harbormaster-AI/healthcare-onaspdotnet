
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Api;

public static class ClinicianEndpoints
{
    public static IEndpointRouteBuilder MapClinicianEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/clinician").WithTags("Clinicians");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToCareTeams", AddToCareTeams);
    group.MapPut("/removeFromCareTeams", RemoveFromCareTeams);

    group.MapPut("/addToAppointments", AddToAppointments);
    group.MapPut("/removeFromAppointments", RemoveFromAppointments);

    group.MapPut("/addToEncounters", AddToEncounters);
    group.MapPut("/removeFromEncounters", RemoveFromEncounters);

    group.MapPut("/addToProcedures", AddToProcedures);
    group.MapPut("/removeFromProcedures", RemoveFromProcedures);

    group.MapPut("/addToImagingReports", AddToImagingReports);
    group.MapPut("/removeFromImagingReports", RemoveFromImagingReports);


        return app;
    }

    private static async Task<IResult> Create(
        ClinicianRequest request,
        IClinicianService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToClinician( request );

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
        ClinicianRequest request,
        IClinicianService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToClinician( request );

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
        IClinicianService service,
        CancellationToken cancellationToken) {

        var clinician = await service.Get(identifier, cancellationToken);
        return clinician is null ? Results.NotFound() : Results.Ok( clinician );
    }


    private static async Task<IResult> GetAll(
        IClinicianService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ClinicianResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IClinicianService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToCareTeams(
        MultipleAssociationRequest request,
        IClinicianService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCareTeams(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCareTeams(
        MultipleAssociationRequest request,
        IClinicianService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCareTeams(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToAppointments(
        MultipleAssociationRequest request,
        IClinicianService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAppointments(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAppointments(
        MultipleAssociationRequest request,
        IClinicianService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAppointments(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToEncounters(
        MultipleAssociationRequest request,
        IClinicianService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToEncounters(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromEncounters(
        MultipleAssociationRequest request,
        IClinicianService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromEncounters(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToProcedures(
        MultipleAssociationRequest request,
        IClinicianService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToProcedures(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromProcedures(
        MultipleAssociationRequest request,
        IClinicianService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromProcedures(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToImagingReports(
        MultipleAssociationRequest request,
        IClinicianService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToImagingReports(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromImagingReports(
        MultipleAssociationRequest request,
        IClinicianService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromImagingReports(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Clinician mapRequestToClinician( ClinicianRequest request ) {
        var model = new Clinician
        {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            LicenseNumber = request.LicenseNumber,
            ClinicianType = request.ClinicianType,
            Specialty = request.Specialty,
        };
        return model;
    }

}
