
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Api;

public static class EncounterEndpoints
{
    public static IEndpointRouteBuilder MapEncounterEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/encounter").WithTags("Encounters");

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
        group.MapPut("/assignAppointment", AssignAppointment);
        group.MapPut("/unassignAppointment", UnassignAppointment);
        group.MapPut("/assignAdmission", AssignAdmission);
        group.MapPut("/unassignAdmission", UnassignAdmission);
        group.MapPut("/assignDischarge", AssignDischarge);
        group.MapPut("/unassignDischarge", UnassignDischarge);

    group.MapPut("/addToDiagnoses", AddToDiagnoses);
    group.MapPut("/removeFromDiagnoses", RemoveFromDiagnoses);

    group.MapPut("/addToProcedures", AddToProcedures);
    group.MapPut("/removeFromProcedures", RemoveFromProcedures);

    group.MapPut("/addToObservations", AddToObservations);
    group.MapPut("/removeFromObservations", RemoveFromObservations);

    group.MapPut("/addToOrders", AddToOrders);
    group.MapPut("/removeFromOrders", RemoveFromOrders);


        return app;
    }

    private static async Task<IResult> Create(
        EncounterRequest request,
        IEncounterService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToEncounter( request );

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
        EncounterRequest request,
        IEncounterService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToEncounter( request );

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
        IEncounterService service,
        CancellationToken cancellationToken) {

        var encounter = await service.Get(identifier, cancellationToken);
        return encounter is null ? Results.NotFound() : Results.Ok( encounter );
    }


    private static async Task<IResult> GetAll(
        IEncounterService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( EncounterResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IEncounterService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPatient(
        AssociationRequest request,
        IEncounterService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPatient(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPatient(
    AssociationRequest request,
    IEncounterService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPatient(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignClinician(
        AssociationRequest request,
        IEncounterService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignClinician(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignClinician(
    AssociationRequest request,
    IEncounterService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignClinician(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignFacility(
        AssociationRequest request,
        IEncounterService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignFacility(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignFacility(
    AssociationRequest request,
    IEncounterService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignFacility(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAppointment(
        AssociationRequest request,
        IEncounterService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAppointment(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAppointment(
    AssociationRequest request,
    IEncounterService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAppointment(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAdmission(
        AssociationRequest request,
        IEncounterService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAdmission(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAdmission(
    AssociationRequest request,
    IEncounterService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAdmission(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDischarge(
        AssociationRequest request,
        IEncounterService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignDischarge(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignDischarge(
    AssociationRequest request,
    IEncounterService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignDischarge(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToDiagnoses(
        MultipleAssociationRequest request,
        IEncounterService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDiagnoses(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDiagnoses(
        MultipleAssociationRequest request,
        IEncounterService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDiagnoses(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToProcedures(
        MultipleAssociationRequest request,
        IEncounterService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToProcedures(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromProcedures(
        MultipleAssociationRequest request,
        IEncounterService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromProcedures(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToObservations(
        MultipleAssociationRequest request,
        IEncounterService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToObservations(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromObservations(
        MultipleAssociationRequest request,
        IEncounterService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromObservations(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToOrders(
        MultipleAssociationRequest request,
        IEncounterService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToOrders(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromOrders(
        MultipleAssociationRequest request,
        IEncounterService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromOrders(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Encounter mapRequestToEncounter( EncounterRequest request ) {
        var model = new Encounter
        {
            Id = request.Id,
            EncounterNumber = request.EncounterNumber,
            StartDateTime = request.StartDateTime,
            EndDateTime = request.EndDateTime,
            Status = request.Status,
            EncounterType = request.EncounterType,
        };
        return model;
    }

}
