
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Api;

public static class AppointmentEndpoints
{
    public static IEndpointRouteBuilder MapAppointmentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/appointment").WithTags("Appointments");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPatient", AssignPatient);
        group.MapPut("/unassignPatient", UnassignPatient);
        group.MapPut("/assignClinician", AssignClinician);
        group.MapPut("/unassignClinician", UnassignClinician);
        group.MapPut("/assignFacility", AssignFacility);
        group.MapPut("/unassignFacility", UnassignFacility);
        group.MapPut("/assignEncounter", AssignEncounter);
        group.MapPut("/unassignEncounter", UnassignEncounter);


        return app;
    }

    private static async Task<IResult> Create(
        AppointmentRequest request,
        IAppointmentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAppointment( request );

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
        AppointmentRequest request,
        IAppointmentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAppointment( request );

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
        IAppointmentService service,
        CancellationToken cancellationToken) {

        var appointment = await service.Get(identifier, cancellationToken);
        return appointment is null ? Results.NotFound() : Results.Ok( appointment );
    }


    private static async Task<IResult> GetAll(
        IAppointmentService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( AppointmentResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IAppointmentService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPatient(
        AssociationRequest request,
        IAppointmentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPatient(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPatient(
    AssociationRequest request,
    IAppointmentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPatient(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignClinician(
        AssociationRequest request,
        IAppointmentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignClinician(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignClinician(
    AssociationRequest request,
    IAppointmentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignClinician(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignFacility(
        AssociationRequest request,
        IAppointmentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignFacility(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignFacility(
    AssociationRequest request,
    IAppointmentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignFacility(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignEncounter(
        AssociationRequest request,
        IAppointmentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignEncounter(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignEncounter(
    AssociationRequest request,
    IAppointmentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignEncounter(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Appointment mapRequestToAppointment( AppointmentRequest request ) {
        var model = new Appointment
        {
            Id = request.Id,
            AppointmentDate = request.AppointmentDate,
            Reason = request.Reason,
            Status = request.Status,
            Priority = request.Priority,
        };
        return model;
    }

}
