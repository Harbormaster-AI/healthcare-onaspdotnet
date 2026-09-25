
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Api;

public static class InsurancePayerEndpoints
{
    public static IEndpointRouteBuilder MapInsurancePayerEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/insurancePayer").WithTags("InsurancePayers");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToPlans", AddToPlans);
    group.MapPut("/removeFromPlans", RemoveFromPlans);

    group.MapPut("/addToClaims", AddToClaims);
    group.MapPut("/removeFromClaims", RemoveFromClaims);


        return app;
    }

    private static async Task<IResult> Create(
        InsurancePayerRequest request,
        IInsurancePayerService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToInsurancePayer( request );

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
        InsurancePayerRequest request,
        IInsurancePayerService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToInsurancePayer( request );

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
        IInsurancePayerService service,
        CancellationToken cancellationToken) {

        var insurancePayer = await service.Get(identifier, cancellationToken);
        return insurancePayer is null ? Results.NotFound() : Results.Ok( insurancePayer );
    }


    private static async Task<IResult> GetAll(
        IInsurancePayerService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( InsurancePayerResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IInsurancePayerService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToPlans(
        MultipleAssociationRequest request,
        IInsurancePayerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPlans(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPlans(
        MultipleAssociationRequest request,
        IInsurancePayerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPlans(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToClaims(
        MultipleAssociationRequest request,
        IInsurancePayerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToClaims(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromClaims(
        MultipleAssociationRequest request,
        IInsurancePayerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromClaims(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static InsurancePayer mapRequestToInsurancePayer( InsurancePayerRequest request ) {
        var model = new InsurancePayer
        {
            Id = request.Id,
            Name = request.Name,
            Website = request.Website,
            PayerType = request.PayerType,
        };
        return model;
    }

}
