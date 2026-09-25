
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Api;

public static class ImagingReportEndpoints
{
    public static IEndpointRouteBuilder MapImagingReportEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/imagingReport").WithTags("ImagingReports");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignImagingOrder", AssignImagingOrder);
        group.MapPut("/unassignImagingOrder", UnassignImagingOrder);
        group.MapPut("/assignClinician", AssignClinician);
        group.MapPut("/unassignClinician", UnassignClinician);
        group.MapPut("/assignEncounter", AssignEncounter);
        group.MapPut("/unassignEncounter", UnassignEncounter);
        group.MapPut("/assignImagingCenter", AssignImagingCenter);
        group.MapPut("/unassignImagingCenter", UnassignImagingCenter);


        return app;
    }

    private static async Task<IResult> Create(
        ImagingReportRequest request,
        IImagingReportService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToImagingReport( request );

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
        ImagingReportRequest request,
        IImagingReportService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToImagingReport( request );

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
        IImagingReportService service,
        CancellationToken cancellationToken) {

        var imagingReport = await service.Get(identifier, cancellationToken);
        return imagingReport is null ? Results.NotFound() : Results.Ok( imagingReport );
    }


    private static async Task<IResult> GetAll(
        IImagingReportService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ImagingReportResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IImagingReportService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignImagingOrder(
        AssociationRequest request,
        IImagingReportService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignImagingOrder(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignImagingOrder(
    AssociationRequest request,
    IImagingReportService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignImagingOrder(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignClinician(
        AssociationRequest request,
        IImagingReportService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignClinician(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignClinician(
    AssociationRequest request,
    IImagingReportService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignClinician(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignEncounter(
        AssociationRequest request,
        IImagingReportService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignEncounter(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignEncounter(
    AssociationRequest request,
    IImagingReportService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignEncounter(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignImagingCenter(
        AssociationRequest request,
        IImagingReportService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignImagingCenter(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignImagingCenter(
    AssociationRequest request,
    IImagingReportService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignImagingCenter(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static ImagingReport mapRequestToImagingReport( ImagingReportRequest request ) {
        var model = new ImagingReport
        {
            Id = request.Id,
            ReportNumber = request.ReportNumber,
            Impression = request.Impression,
            ReportedDate = request.ReportedDate,
            Status = request.Status,
        };
        return model;
    }

}
