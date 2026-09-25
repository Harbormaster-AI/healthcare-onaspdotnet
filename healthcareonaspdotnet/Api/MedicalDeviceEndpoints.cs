
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Api;

public static class MedicalDeviceEndpoints
{
    public static IEndpointRouteBuilder MapMedicalDeviceEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/medicalDevice").WithTags("MedicalDevices");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPatient", AssignPatient);
        group.MapPut("/unassignPatient", UnassignPatient);

    group.MapPut("/addToObservations", AddToObservations);
    group.MapPut("/removeFromObservations", RemoveFromObservations);

    group.MapPut("/addToSoftwareUpdates", AddToSoftwareUpdates);
    group.MapPut("/removeFromSoftwareUpdates", RemoveFromSoftwareUpdates);


        return app;
    }

    private static async Task<IResult> Create(
        MedicalDeviceRequest request,
        IMedicalDeviceService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToMedicalDevice( request );

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
        MedicalDeviceRequest request,
        IMedicalDeviceService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToMedicalDevice( request );

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
        IMedicalDeviceService service,
        CancellationToken cancellationToken) {

        var medicalDevice = await service.Get(identifier, cancellationToken);
        return medicalDevice is null ? Results.NotFound() : Results.Ok( medicalDevice );
    }


    private static async Task<IResult> GetAll(
        IMedicalDeviceService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( MedicalDeviceResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IMedicalDeviceService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPatient(
        AssociationRequest request,
        IMedicalDeviceService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPatient(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPatient(
    AssociationRequest request,
    IMedicalDeviceService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPatient(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToObservations(
        MultipleAssociationRequest request,
        IMedicalDeviceService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToObservations(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromObservations(
        MultipleAssociationRequest request,
        IMedicalDeviceService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromObservations(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToSoftwareUpdates(
        MultipleAssociationRequest request,
        IMedicalDeviceService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToSoftwareUpdates(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromSoftwareUpdates(
        MultipleAssociationRequest request,
        IMedicalDeviceService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromSoftwareUpdates(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static MedicalDevice mapRequestToMedicalDevice( MedicalDeviceRequest request ) {
        var model = new MedicalDevice
        {
            Id = request.Id,
            Udi = request.Udi,
            Manufacturer = request.Manufacturer,
            DeviceType = request.DeviceType,
            ConnectivityStatus = request.ConnectivityStatus,
        };
        return model;
    }

}
