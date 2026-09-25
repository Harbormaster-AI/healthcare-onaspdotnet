
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Api;

public static class FacilityEndpoints
{
    public static IEndpointRouteBuilder MapFacilityEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/facility").WithTags("Facilitys");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignHealthSystem", AssignHealthSystem);
        group.MapPut("/unassignHealthSystem", UnassignHealthSystem);

    group.MapPut("/addToDepartments", AddToDepartments);
    group.MapPut("/removeFromDepartments", RemoveFromDepartments);

    group.MapPut("/addToCareTeams", AddToCareTeams);
    group.MapPut("/removeFromCareTeams", RemoveFromCareTeams);

    group.MapPut("/addToLaboratories", AddToLaboratories);
    group.MapPut("/removeFromLaboratories", RemoveFromLaboratories);

    group.MapPut("/addToImagingCenters", AddToImagingCenters);
    group.MapPut("/removeFromImagingCenters", RemoveFromImagingCenters);

    group.MapPut("/addToPharmacies", AddToPharmacies);
    group.MapPut("/removeFromPharmacies", RemoveFromPharmacies);

    group.MapPut("/addToInventoryItems", AddToInventoryItems);
    group.MapPut("/removeFromInventoryItems", RemoveFromInventoryItems);


        return app;
    }

    private static async Task<IResult> Create(
        FacilityRequest request,
        IFacilityService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToFacility( request );

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
        FacilityRequest request,
        IFacilityService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToFacility( request );

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
        IFacilityService service,
        CancellationToken cancellationToken) {

        var facility = await service.Get(identifier, cancellationToken);
        return facility is null ? Results.NotFound() : Results.Ok( facility );
    }


    private static async Task<IResult> GetAll(
        IFacilityService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( FacilityResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IFacilityService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignHealthSystem(
        AssociationRequest request,
        IFacilityService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignHealthSystem(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignHealthSystem(
    AssociationRequest request,
    IFacilityService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignHealthSystem(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToDepartments(
        MultipleAssociationRequest request,
        IFacilityService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDepartments(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDepartments(
        MultipleAssociationRequest request,
        IFacilityService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDepartments(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToCareTeams(
        MultipleAssociationRequest request,
        IFacilityService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCareTeams(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCareTeams(
        MultipleAssociationRequest request,
        IFacilityService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCareTeams(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToLaboratories(
        MultipleAssociationRequest request,
        IFacilityService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToLaboratories(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromLaboratories(
        MultipleAssociationRequest request,
        IFacilityService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromLaboratories(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToImagingCenters(
        MultipleAssociationRequest request,
        IFacilityService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToImagingCenters(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromImagingCenters(
        MultipleAssociationRequest request,
        IFacilityService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromImagingCenters(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToPharmacies(
        MultipleAssociationRequest request,
        IFacilityService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPharmacies(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPharmacies(
        MultipleAssociationRequest request,
        IFacilityService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPharmacies(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToInventoryItems(
        MultipleAssociationRequest request,
        IFacilityService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToInventoryItems(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromInventoryItems(
        MultipleAssociationRequest request,
        IFacilityService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromInventoryItems(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Facility mapRequestToFacility( FacilityRequest request ) {
        var model = new Facility
        {
            Id = request.Id,
            Name = request.Name,
            FacilityCode = request.FacilityCode,
            Address = request.Address,
            FacilityType = request.FacilityType,
        };
        return model;
    }

}
