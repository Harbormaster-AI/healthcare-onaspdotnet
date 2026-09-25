
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Api;

public static class ClinicalOrderEndpoints
{
    public static IEndpointRouteBuilder MapClinicalOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/clinicalOrder").WithTags("ClinicalOrders");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPatient", AssignPatient);
        group.MapPut("/unassignPatient", UnassignPatient);
        group.MapPut("/assignEncounter", AssignEncounter);
        group.MapPut("/unassignEncounter", UnassignEncounter);
        group.MapPut("/assignOrderingClinician", AssignOrderingClinician);
        group.MapPut("/unassignOrderingClinician", UnassignOrderingClinician);

    group.MapPut("/addToMedicationOrders", AddToMedicationOrders);
    group.MapPut("/removeFromMedicationOrders", RemoveFromMedicationOrders);

    group.MapPut("/addToLaboratoryOrders", AddToLaboratoryOrders);
    group.MapPut("/removeFromLaboratoryOrders", RemoveFromLaboratoryOrders);

    group.MapPut("/addToImagingOrders", AddToImagingOrders);
    group.MapPut("/removeFromImagingOrders", RemoveFromImagingOrders);

    group.MapPut("/addToProcedureOrders", AddToProcedureOrders);
    group.MapPut("/removeFromProcedureOrders", RemoveFromProcedureOrders);

    group.MapPut("/addToAuthorizations", AddToAuthorizations);
    group.MapPut("/removeFromAuthorizations", RemoveFromAuthorizations);


        return app;
    }

    private static async Task<IResult> Create(
        ClinicalOrderRequest request,
        IClinicalOrderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToClinicalOrder( request );

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
        ClinicalOrderRequest request,
        IClinicalOrderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToClinicalOrder( request );

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
        IClinicalOrderService service,
        CancellationToken cancellationToken) {

        var clinicalOrder = await service.Get(identifier, cancellationToken);
        return clinicalOrder is null ? Results.NotFound() : Results.Ok( clinicalOrder );
    }


    private static async Task<IResult> GetAll(
        IClinicalOrderService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ClinicalOrderResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IClinicalOrderService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPatient(
        AssociationRequest request,
        IClinicalOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPatient(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPatient(
    AssociationRequest request,
    IClinicalOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPatient(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignEncounter(
        AssociationRequest request,
        IClinicalOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignEncounter(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignEncounter(
    AssociationRequest request,
    IClinicalOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignEncounter(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOrderingClinician(
        AssociationRequest request,
        IClinicalOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOrderingClinician(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOrderingClinician(
    AssociationRequest request,
    IClinicalOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOrderingClinician(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToMedicationOrders(
        MultipleAssociationRequest request,
        IClinicalOrderService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToMedicationOrders(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromMedicationOrders(
        MultipleAssociationRequest request,
        IClinicalOrderService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromMedicationOrders(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToLaboratoryOrders(
        MultipleAssociationRequest request,
        IClinicalOrderService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToLaboratoryOrders(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromLaboratoryOrders(
        MultipleAssociationRequest request,
        IClinicalOrderService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromLaboratoryOrders(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToImagingOrders(
        MultipleAssociationRequest request,
        IClinicalOrderService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToImagingOrders(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromImagingOrders(
        MultipleAssociationRequest request,
        IClinicalOrderService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromImagingOrders(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToProcedureOrders(
        MultipleAssociationRequest request,
        IClinicalOrderService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToProcedureOrders(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromProcedureOrders(
        MultipleAssociationRequest request,
        IClinicalOrderService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromProcedureOrders(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToAuthorizations(
        MultipleAssociationRequest request,
        IClinicalOrderService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAuthorizations(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAuthorizations(
        MultipleAssociationRequest request,
        IClinicalOrderService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAuthorizations(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static ClinicalOrder mapRequestToClinicalOrder( ClinicalOrderRequest request ) {
        var model = new ClinicalOrder
        {
            Id = request.Id,
            OrderNumber = request.OrderNumber,
            Status = request.Status,
            OrderType = request.OrderType,
            Priority = request.Priority,
        };
        return model;
    }

}
