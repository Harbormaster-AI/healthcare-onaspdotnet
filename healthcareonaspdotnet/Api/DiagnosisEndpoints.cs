
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Api;

public static class DiagnosisEndpoints
{
    public static IEndpointRouteBuilder MapDiagnosisEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/diagnosis").WithTags("Diagnosiss");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignEncounter", AssignEncounter);
        group.MapPut("/unassignEncounter", UnassignEncounter);
        group.MapPut("/assignPatient", AssignPatient);
        group.MapPut("/unassignPatient", UnassignPatient);


        return app;
    }

    private static async Task<IResult> Create(
        DiagnosisRequest request,
        IDiagnosisService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDiagnosis( request );

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
        DiagnosisRequest request,
        IDiagnosisService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDiagnosis( request );

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
        IDiagnosisService service,
        CancellationToken cancellationToken) {

        var diagnosis = await service.Get(identifier, cancellationToken);
        return diagnosis is null ? Results.NotFound() : Results.Ok( diagnosis );
    }


    private static async Task<IResult> GetAll(
        IDiagnosisService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( DiagnosisResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IDiagnosisService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignEncounter(
        AssociationRequest request,
        IDiagnosisService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignEncounter(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignEncounter(
    AssociationRequest request,
    IDiagnosisService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignEncounter(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPatient(
        AssociationRequest request,
        IDiagnosisService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPatient(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPatient(
    AssociationRequest request,
    IDiagnosisService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPatient(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Diagnosis mapRequestToDiagnosis( DiagnosisRequest request ) {
        var model = new Diagnosis
        {
            Id = request.Id,
            Code = request.Code,
            Description = request.Description,
            OnsetDate = request.OnsetDate,
            Certainty = request.Certainty,
        };
        return model;
    }

}
