using healthcareonaspdotnet.Api;
using healthcareonaspdotnet.Persistence;
using healthcareonaspdotnet.Service;
using healthcareonaspdotnet.Telemetry;
using OpenTelemetry.Metrics;
using Microsoft.EntityFrameworkCore;
using Oracle.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddOpenApi();
builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>();

var dbEngineEnvironment = builder.Configuration["DB_TYPE"];

string dbEngine;
string dbUserName;
string dbPassword;
string dbName;
string dbHost;
string dbPort;

if (string.IsNullOrWhiteSpace(dbEngineEnvironment))
{
    // Generation-time configuration
    dbEngine = "mysql";
    dbUserName = "postgres";
    dbPassword = "no_password";
    dbName = "testDb";
    dbHost = "localhost";
    dbPort = "5432";
}
else
{
    // Runtime environment configuration
    dbEngine = dbEngineEnvironment;
    dbUserName = builder.Configuration["DB_USER_NAME"]
        ?? throw new InvalidOperationException("DB_USER_NAME is required when DB_ENGINE is provided.");

    dbPassword = builder.Configuration["DB_PASSWORD"]
        ?? throw new InvalidOperationException("DB_PASSWORD is required when DB_ENGINE is provided.");

    dbName = builder.Configuration["DB_NAME"]
        ?? throw new InvalidOperationException("DB_NAME is required when DB_ENGINE is provided.");

    dbHost = builder.Configuration["DB_HOST"]
        ?? throw new InvalidOperationException("DB_HOST is required when DB_ENGINE is provided.");

    dbPort = builder.Configuration["DB_PORT"]
        ?? throw new InvalidOperationException("DB_PORT is required when DB_ENGINE is provided.");
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString =
        $"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUserName};Password={dbPassword}";

    if (dbEngine.Equals("mysql", StringComparison.OrdinalIgnoreCase))
    {
        options.UseMySQL(connectionString);
    }
    else if (dbEngine.Equals("mariadb", StringComparison.OrdinalIgnoreCase))
    {
        options.UseMySQL(connectionString);
    }
    else if (dbEngine.Equals("sqlite", StringComparison.OrdinalIgnoreCase))
    {
        options.UseSqlite(connectionString);
    }
    else if (dbEngine.Equals("sqlserver", StringComparison.OrdinalIgnoreCase) ||
             dbEngine.Equals("azuresql", StringComparison.OrdinalIgnoreCase))
    {
        options.UseSqlServer(connectionString);
    }
    else if (dbEngine.Equals("oracle", StringComparison.OrdinalIgnoreCase))
    {
        options.UseOracle(connectionString);
    }
    else if (dbEngine.Equals("mongodb", StringComparison.OrdinalIgnoreCase))
    {
        options.UseMongoDB(connectionString);
    }
    else if (dbEngine.Equals("inmemory", StringComparison.OrdinalIgnoreCase))
    {
        options.UseInMemoryDatabase("healthcareonaspdotnet");
    }
    else if (dbEngine.Equals("cosmosdb", StringComparison.OrdinalIgnoreCase))
    {
        var cosmosEndpoint = builder.Configuration["DB_ENDPOINT"]
            ?? throw new InvalidOperationException(
                "DB_ENDPOINT is required for Cosmos DB.");

        var cosmosKey = builder.Configuration["DB_KEY"]
            ?? throw new InvalidOperationException(
                "DB_KEY is required for Cosmos DB.");

        options.UseCosmos(
            cosmosEndpoint,
            cosmosKey,
            dbName);
    }
    else if (dbEngine.Equals("postgres", StringComparison.OrdinalIgnoreCase))
    {
        options.UseNpgsql(connectionString);
    }
    else
    {
        throw new InvalidOperationException(
            "Unsupported or missing database engine configuration.");
    }
    // etc.
});

    builder.Services.AddScoped<IHealthSystemRepository, HealthSystemRepository>();
    builder.Services.AddScoped<IFacilityRepository, FacilityRepository>();
    builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
    builder.Services.AddScoped<ICareTeamRepository, CareTeamRepository>();
    builder.Services.AddScoped<IClinicianRepository, ClinicianRepository>();
    builder.Services.AddScoped<IPatientRepository, PatientRepository>();
    builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
    builder.Services.AddScoped<IEncounterRepository, EncounterRepository>();
    builder.Services.AddScoped<IAdmissionRepository, AdmissionRepository>();
    builder.Services.AddScoped<IDischargeRepository, DischargeRepository>();
    builder.Services.AddScoped<IClinicalOrderRepository, ClinicalOrderRepository>();
    builder.Services.AddScoped<IMedicationOrderRepository, MedicationOrderRepository>();
    builder.Services.AddScoped<ILaboratoryRepository, LaboratoryRepository>();
    builder.Services.AddScoped<ILaboratoryOrderRepository, LaboratoryOrderRepository>();
    builder.Services.AddScoped<ILabResultRepository, LabResultRepository>();
    builder.Services.AddScoped<IImagingCenterRepository, ImagingCenterRepository>();
    builder.Services.AddScoped<IImagingOrderRepository, ImagingOrderRepository>();
    builder.Services.AddScoped<IImagingReportRepository, ImagingReportRepository>();
    builder.Services.AddScoped<IProcedureOrderRepository, ProcedureOrderRepository>();
    builder.Services.AddScoped<IProcedureRepository, ProcedureRepository>();
    builder.Services.AddScoped<IPharmacyRepository, PharmacyRepository>();
    builder.Services.AddScoped<IMedicationDispenseRepository, MedicationDispenseRepository>();
    builder.Services.AddScoped<IDiagnosisRepository, DiagnosisRepository>();
    builder.Services.AddScoped<IObservationRepository, ObservationRepository>();
    builder.Services.AddScoped<ICarePlanRepository, CarePlanRepository>();
    builder.Services.AddScoped<ICareTaskRepository, CareTaskRepository>();
    builder.Services.AddScoped<IAllergyRepository, AllergyRepository>();
    builder.Services.AddScoped<IConditionRepository, ConditionRepository>();
    builder.Services.AddScoped<IInsurancePayerRepository, InsurancePayerRepository>();
    builder.Services.AddScoped<IInsurancePlanRepository, InsurancePlanRepository>();
    builder.Services.AddScoped<ICoverageRepository, CoverageRepository>();
    builder.Services.AddScoped<IClaimRepository, ClaimRepository>();
    builder.Services.AddScoped<IAuthorizationRepository, AuthorizationRepository>();
    builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
    builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
    builder.Services.AddScoped<IMedicalDeviceRepository, MedicalDeviceRepository>();
    builder.Services.AddScoped<ISoftwareUpdateRepository, SoftwareUpdateRepository>();
    builder.Services.AddScoped<IMedicalSupplierRepository, MedicalSupplierRepository>();
    builder.Services.AddScoped<IInventoryItemRepository, InventoryItemRepository>();

    builder.Services.AddScoped<IHealthSystemService, HealthSystemService>();
    builder.Services.AddScoped<IFacilityService, FacilityService>();
    builder.Services.AddScoped<IDepartmentService, DepartmentService>();
    builder.Services.AddScoped<ICareTeamService, CareTeamService>();
    builder.Services.AddScoped<IClinicianService, ClinicianService>();
    builder.Services.AddScoped<IPatientService, PatientService>();
    builder.Services.AddScoped<IAppointmentService, AppointmentService>();
    builder.Services.AddScoped<IEncounterService, EncounterService>();
    builder.Services.AddScoped<IAdmissionService, AdmissionService>();
    builder.Services.AddScoped<IDischargeService, DischargeService>();
    builder.Services.AddScoped<IClinicalOrderService, ClinicalOrderService>();
    builder.Services.AddScoped<IMedicationOrderService, MedicationOrderService>();
    builder.Services.AddScoped<ILaboratoryService, LaboratoryService>();
    builder.Services.AddScoped<ILaboratoryOrderService, LaboratoryOrderService>();
    builder.Services.AddScoped<ILabResultService, LabResultService>();
    builder.Services.AddScoped<IImagingCenterService, ImagingCenterService>();
    builder.Services.AddScoped<IImagingOrderService, ImagingOrderService>();
    builder.Services.AddScoped<IImagingReportService, ImagingReportService>();
    builder.Services.AddScoped<IProcedureOrderService, ProcedureOrderService>();
    builder.Services.AddScoped<IProcedureService, ProcedureService>();
    builder.Services.AddScoped<IPharmacyService, PharmacyService>();
    builder.Services.AddScoped<IMedicationDispenseService, MedicationDispenseService>();
    builder.Services.AddScoped<IDiagnosisService, DiagnosisService>();
    builder.Services.AddScoped<IObservationService, ObservationService>();
    builder.Services.AddScoped<ICarePlanService, CarePlanService>();
    builder.Services.AddScoped<ICareTaskService, CareTaskService>();
    builder.Services.AddScoped<IAllergyService, AllergyService>();
    builder.Services.AddScoped<IConditionService, ConditionService>();
    builder.Services.AddScoped<IInsurancePayerService, InsurancePayerService>();
    builder.Services.AddScoped<IInsurancePlanService, InsurancePlanService>();
    builder.Services.AddScoped<ICoverageService, CoverageService>();
    builder.Services.AddScoped<IClaimService, ClaimService>();
    builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
    builder.Services.AddScoped<IInvoiceService, InvoiceService>();
    builder.Services.AddScoped<IPaymentService, PaymentService>();
    builder.Services.AddScoped<IMedicalDeviceService, MedicalDeviceService>();
    builder.Services.AddScoped<ISoftwareUpdateService, SoftwareUpdateService>();
    builder.Services.AddScoped<IMedicalSupplierService, MedicalSupplierService>();
    builder.Services.AddScoped<IInventoryItemService, InventoryItemService>();

// apply the service resolver
builder.Services.AddScoped<IServiceResolver, ServiceResolver>();

builder.Services.AddSingleton<ApplicationTelemetry>();

builder.Services
    .AddOpenTelemetry()
    .WithMetrics(metrics =>
    {
        metrics.AddMeter("Harbormaster.Application");

        metrics.AddPrometheusExporter();
    });

var app = builder.Build();

app.MapPrometheusScrapingEndpoint();


// Health endpoint
app.MapHealthChecks("/health");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // Testing uses a shared SQLite file; wipe it so unique indexes (e.g. email) don't fail on re-runs.
    if (app.Environment.IsEnvironment("Testing"))
    {
        db.Database.EnsureDeleted();
    }

    // EnsureCreated does not alter an existing schema. If you changed relationships locally,
    // recreate the MySQL database (docker compose down -v && docker compose up -d).
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


    app.MapHealthSystemEndpoints();
    app.MapFacilityEndpoints();
    app.MapDepartmentEndpoints();
    app.MapCareTeamEndpoints();
    app.MapClinicianEndpoints();
    app.MapPatientEndpoints();
    app.MapAppointmentEndpoints();
    app.MapEncounterEndpoints();
    app.MapAdmissionEndpoints();
    app.MapDischargeEndpoints();
    app.MapClinicalOrderEndpoints();
    app.MapMedicationOrderEndpoints();
    app.MapLaboratoryEndpoints();
    app.MapLaboratoryOrderEndpoints();
    app.MapLabResultEndpoints();
    app.MapImagingCenterEndpoints();
    app.MapImagingOrderEndpoints();
    app.MapImagingReportEndpoints();
    app.MapProcedureOrderEndpoints();
    app.MapProcedureEndpoints();
    app.MapPharmacyEndpoints();
    app.MapMedicationDispenseEndpoints();
    app.MapDiagnosisEndpoints();
    app.MapObservationEndpoints();
    app.MapCarePlanEndpoints();
    app.MapCareTaskEndpoints();
    app.MapAllergyEndpoints();
    app.MapConditionEndpoints();
    app.MapInsurancePayerEndpoints();
    app.MapInsurancePlanEndpoints();
    app.MapCoverageEndpoints();
    app.MapClaimEndpoints();
    app.MapAuthorizationEndpoints();
    app.MapInvoiceEndpoints();
    app.MapPaymentEndpoints();
    app.MapMedicalDeviceEndpoints();
    app.MapSoftwareUpdateEndpoints();
    app.MapMedicalSupplierEndpoints();
    app.MapInventoryItemEndpoints();

app.Run();

public partial class Program { }

