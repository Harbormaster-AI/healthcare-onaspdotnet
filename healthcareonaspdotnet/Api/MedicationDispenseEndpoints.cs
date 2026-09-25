
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Api;

public static class MedicationDispenseEndpoints
{
    public static IEndpointRouteBuilder MapMedicationDispenseEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/medicationDispense").WithTags("MedicationDispenses");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignMedicationOrder", AssignMedicationOrder);
        group.MapPut("/unassignMedicationOrder", UnassignMedicationOrder);
        group.MapPut("/assignPharmacy", AssignPharmacy);
        group.MapPut("/unassignPharmacy", UnassignPharmacy);
        group.MapPut("/assignPatient", AssignPatient);
        group.MapPut("/unassignPatient", UnassignPatient);


        return app;
    }

    private static async Task<IResult> Create(
        MedicationDispenseRequest request,
        IMedicationDispenseService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToMedicationDispense( request );

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
        MedicationDispenseRequest request,
        IMedicationDispenseService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToMedicationDispense( request );

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
        IMedicationDispenseService service,
        CancellationToken cancellationToken) {

        var medicationDispense = await service.Get(identifier, cancellationToken);
        return medicationDispense is null ? Results.NotFound() : Results.Ok( medicationDispense );
    }


    private static async Task<IResult> GetAll(
        IMedicationDispenseService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( MedicationDispenseResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IMedicationDispenseService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignMedicationOrder(
        AssociationRequest request,
        IMedicationDispenseService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignMedicationOrder(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignMedicationOrder(
    AssociationRequest request,
    IMedicationDispenseService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignMedicationOrder(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPharmacy(
        AssociationRequest request,
        IMedicationDispenseService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPharmacy(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPharmacy(
    AssociationRequest request,
    IMedicationDispenseService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPharmacy(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPatient(
        AssociationRequest request,
        IMedicationDispenseService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPatient(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPatient(
    AssociationRequest request,
    IMedicationDispenseService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPatient(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static MedicationDispense mapRequestToMedicationDispense( MedicationDispenseRequest request ) {
        var model = new MedicationDispense
        {
            Id = request.Id,
            DispenseNumber = request.DispenseNumber,
            Quantity = request.Quantity,
            WhenPrepared = request.WhenPrepared,
            Status = request.Status,
        };
        return model;
    }

}
