
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Api;

public static class InsurancePlanEndpoints
{
    public static IEndpointRouteBuilder MapInsurancePlanEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/insurancePlan").WithTags("InsurancePlans");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPayer", AssignPayer);
        group.MapPut("/unassignPayer", UnassignPayer);

    group.MapPut("/addToCoverages", AddToCoverages);
    group.MapPut("/removeFromCoverages", RemoveFromCoverages);


        return app;
    }

    private static async Task<IResult> Create(
        InsurancePlanRequest request,
        IInsurancePlanService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToInsurancePlan( request );

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
        InsurancePlanRequest request,
        IInsurancePlanService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToInsurancePlan( request );

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
        IInsurancePlanService service,
        CancellationToken cancellationToken) {

        var insurancePlan = await service.Get(identifier, cancellationToken);
        return insurancePlan is null ? Results.NotFound() : Results.Ok( insurancePlan );
    }


    private static async Task<IResult> GetAll(
        IInsurancePlanService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( InsurancePlanResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IInsurancePlanService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPayer(
        AssociationRequest request,
        IInsurancePlanService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPayer(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPayer(
    AssociationRequest request,
    IInsurancePlanService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPayer(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToCoverages(
        MultipleAssociationRequest request,
        IInsurancePlanService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCoverages(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCoverages(
        MultipleAssociationRequest request,
        IInsurancePlanService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCoverages(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static InsurancePlan mapRequestToInsurancePlan( InsurancePlanRequest request ) {
        var model = new InsurancePlan
        {
            Id = request.Id,
            Name = request.Name,
            PlanCode = request.PlanCode,
            PlanType = request.PlanType,
        };
        return model;
    }

}
