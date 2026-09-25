
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Domain;
using healthcareonaspdotnet.Contracts;

namespace healthcareonaspdotnet.Api;

public static class HealthSystemEndpoints
{
    public static IEndpointRouteBuilder MapHealthSystemEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/healthSystem").WithTags("HealthSystems");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToFacilities", AddToFacilities);
    group.MapPut("/removeFromFacilities", RemoveFromFacilities);

    group.MapPut("/addToSuppliers", AddToSuppliers);
    group.MapPut("/removeFromSuppliers", RemoveFromSuppliers);


        return app;
    }

    private static async Task<IResult> Create(
        HealthSystemRequest request,
        IHealthSystemService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToHealthSystem( request );

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
        HealthSystemRequest request,
        IHealthSystemService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToHealthSystem( request );

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
        IHealthSystemService service,
        CancellationToken cancellationToken) {

        var healthSystem = await service.Get(identifier, cancellationToken);
        return healthSystem is null ? Results.NotFound() : Results.Ok( healthSystem );
    }


    private static async Task<IResult> GetAll(
        IHealthSystemService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( HealthSystemResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IHealthSystemService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToFacilities(
        MultipleAssociationRequest request,
        IHealthSystemService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToFacilities(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromFacilities(
        MultipleAssociationRequest request,
        IHealthSystemService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromFacilities(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToSuppliers(
        MultipleAssociationRequest request,
        IHealthSystemService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToSuppliers(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromSuppliers(
        MultipleAssociationRequest request,
        IHealthSystemService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromSuppliers(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static HealthSystem mapRequestToHealthSystem( HealthSystemRequest request ) {
        var model = new HealthSystem
        {
            Id = request.Id,
            Name = request.Name,
            LegalName = request.LegalName,
            HeadquartersCountry = request.HeadquartersCountry,
            Website = request.Website,
        };
        return model;
    }

}
