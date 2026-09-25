
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Api;

public static class AllergyEndpoints
{
    public static IEndpointRouteBuilder MapAllergyEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/allergy").WithTags("Allergys");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPatient", AssignPatient);
        group.MapPut("/unassignPatient", UnassignPatient);


        return app;
    }

    private static async Task<IResult> Create(
        AllergyRequest request,
        IAllergyService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAllergy( request );

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
        AllergyRequest request,
        IAllergyService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAllergy( request );

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
        IAllergyService service,
        CancellationToken cancellationToken) {

        var allergy = await service.Get(identifier, cancellationToken);
        return allergy is null ? Results.NotFound() : Results.Ok( allergy );
    }


    private static async Task<IResult> GetAll(
        IAllergyService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( AllergyResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IAllergyService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPatient(
        AssociationRequest request,
        IAllergyService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPatient(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPatient(
    AssociationRequest request,
    IAllergyService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPatient(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Allergy mapRequestToAllergy( AllergyRequest request ) {
        var model = new Allergy
        {
            Id = request.Id,
            Substance = request.Substance,
            Reaction = request.Reaction,
            Severity = request.Severity,
            Status = request.Status,
        };
        return model;
    }

}
