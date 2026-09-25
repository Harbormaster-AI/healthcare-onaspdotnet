
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Api;

public static class PatientEndpoints
{
    public static IEndpointRouteBuilder MapPatientEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/patient").WithTags("Patients");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToAppointments", AddToAppointments);
    group.MapPut("/removeFromAppointments", RemoveFromAppointments);

    group.MapPut("/addToEncounters", AddToEncounters);
    group.MapPut("/removeFromEncounters", RemoveFromEncounters);

    group.MapPut("/addToCarePlans", AddToCarePlans);
    group.MapPut("/removeFromCarePlans", RemoveFromCarePlans);

    group.MapPut("/addToAllergies", AddToAllergies);
    group.MapPut("/removeFromAllergies", RemoveFromAllergies);

    group.MapPut("/addToConditions", AddToConditions);
    group.MapPut("/removeFromConditions", RemoveFromConditions);

    group.MapPut("/addToMedicationOrders", AddToMedicationOrders);
    group.MapPut("/removeFromMedicationOrders", RemoveFromMedicationOrders);

    group.MapPut("/addToLabOrders", AddToLabOrders);
    group.MapPut("/removeFromLabOrders", RemoveFromLabOrders);

    group.MapPut("/addToImagingOrders", AddToImagingOrders);
    group.MapPut("/removeFromImagingOrders", RemoveFromImagingOrders);

    group.MapPut("/addToCoverages", AddToCoverages);
    group.MapPut("/removeFromCoverages", RemoveFromCoverages);

    group.MapPut("/addToClaims", AddToClaims);
    group.MapPut("/removeFromClaims", RemoveFromClaims);

    group.MapPut("/addToDevices", AddToDevices);
    group.MapPut("/removeFromDevices", RemoveFromDevices);

    group.MapPut("/addToObservations", AddToObservations);
    group.MapPut("/removeFromObservations", RemoveFromObservations);


        return app;
    }

    private static async Task<IResult> Create(
        PatientRequest request,
        IPatientService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPatient( request );

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
        PatientRequest request,
        IPatientService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPatient( request );

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
        IPatientService service,
        CancellationToken cancellationToken) {

        var patient = await service.Get(identifier, cancellationToken);
        return patient is null ? Results.NotFound() : Results.Ok( patient );
    }


    private static async Task<IResult> GetAll(
        IPatientService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( PatientResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IPatientService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToAppointments(
        MultipleAssociationRequest request,
        IPatientService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAppointments(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAppointments(
        MultipleAssociationRequest request,
        IPatientService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAppointments(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToEncounters(
        MultipleAssociationRequest request,
        IPatientService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToEncounters(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromEncounters(
        MultipleAssociationRequest request,
        IPatientService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromEncounters(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToCarePlans(
        MultipleAssociationRequest request,
        IPatientService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCarePlans(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCarePlans(
        MultipleAssociationRequest request,
        IPatientService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCarePlans(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToAllergies(
        MultipleAssociationRequest request,
        IPatientService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAllergies(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAllergies(
        MultipleAssociationRequest request,
        IPatientService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAllergies(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToConditions(
        MultipleAssociationRequest request,
        IPatientService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToConditions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromConditions(
        MultipleAssociationRequest request,
        IPatientService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromConditions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToMedicationOrders(
        MultipleAssociationRequest request,
        IPatientService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToMedicationOrders(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromMedicationOrders(
        MultipleAssociationRequest request,
        IPatientService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromMedicationOrders(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToLabOrders(
        MultipleAssociationRequest request,
        IPatientService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToLabOrders(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromLabOrders(
        MultipleAssociationRequest request,
        IPatientService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromLabOrders(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToImagingOrders(
        MultipleAssociationRequest request,
        IPatientService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToImagingOrders(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromImagingOrders(
        MultipleAssociationRequest request,
        IPatientService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromImagingOrders(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToCoverages(
        MultipleAssociationRequest request,
        IPatientService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCoverages(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCoverages(
        MultipleAssociationRequest request,
        IPatientService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCoverages(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToClaims(
        MultipleAssociationRequest request,
        IPatientService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToClaims(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromClaims(
        MultipleAssociationRequest request,
        IPatientService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromClaims(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToDevices(
        MultipleAssociationRequest request,
        IPatientService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDevices(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDevices(
        MultipleAssociationRequest request,
        IPatientService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDevices(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToObservations(
        MultipleAssociationRequest request,
        IPatientService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToObservations(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromObservations(
        MultipleAssociationRequest request,
        IPatientService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromObservations(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Patient mapRequestToPatient( PatientRequest request ) {
        var model = new Patient
        {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Mrn = request.Mrn,
            DateOfBirth = request.DateOfBirth,
            Address = request.Address,
            PrimaryLanguage = request.PrimaryLanguage,
            SexAtBirth = request.SexAtBirth,
            BloodType = request.BloodType,
        };
        return model;
    }

}
