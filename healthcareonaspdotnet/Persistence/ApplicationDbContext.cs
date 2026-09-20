using Microsoft.EntityFrameworkCore;

using healthcareonaspdotnet.Domain;

namespace healthcareonaspdotnet.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

public DbSet<HealthSystem> HealthSystems => Set<HealthSystem>();
public DbSet<Facility> Facilitys => Set<Facility>();
public DbSet<Department> Departments => Set<Department>();
public DbSet<CareTeam> CareTeams => Set<CareTeam>();
public DbSet<Clinician> Clinicians => Set<Clinician>();
public DbSet<Patient> Patients => Set<Patient>();
public DbSet<Appointment> Appointments => Set<Appointment>();
public DbSet<Encounter> Encounters => Set<Encounter>();
public DbSet<Admission> Admissions => Set<Admission>();
public DbSet<Discharge> Discharges => Set<Discharge>();
public DbSet<ClinicalOrder> ClinicalOrders => Set<ClinicalOrder>();
public DbSet<MedicationOrder> MedicationOrders => Set<MedicationOrder>();
public DbSet<Laboratory> Laboratorys => Set<Laboratory>();
public DbSet<LaboratoryOrder> LaboratoryOrders => Set<LaboratoryOrder>();
public DbSet<LabResult> LabResults => Set<LabResult>();
public DbSet<ImagingCenter> ImagingCenters => Set<ImagingCenter>();
public DbSet<ImagingOrder> ImagingOrders => Set<ImagingOrder>();
public DbSet<ImagingReport> ImagingReports => Set<ImagingReport>();
public DbSet<ProcedureOrder> ProcedureOrders => Set<ProcedureOrder>();
public DbSet<Procedure> Procedures => Set<Procedure>();
public DbSet<Pharmacy> Pharmacys => Set<Pharmacy>();
public DbSet<MedicationDispense> MedicationDispenses => Set<MedicationDispense>();
public DbSet<Diagnosis> Diagnosiss => Set<Diagnosis>();
public DbSet<Observation> Observations => Set<Observation>();
public DbSet<CarePlan> CarePlans => Set<CarePlan>();
public DbSet<CareTask> CareTasks => Set<CareTask>();
public DbSet<Allergy> Allergys => Set<Allergy>();
public DbSet<Condition> Conditions => Set<Condition>();
public DbSet<InsurancePayer> InsurancePayers => Set<InsurancePayer>();
public DbSet<InsurancePlan> InsurancePlans => Set<InsurancePlan>();
public DbSet<Coverage> Coverages => Set<Coverage>();
public DbSet<Claim> Claims => Set<Claim>();
public DbSet<Authorization> Authorizations => Set<Authorization>();
public DbSet<Invoice> Invoices => Set<Invoice>();
public DbSet<Payment> Payments => Set<Payment>();
public DbSet<MedicalDevice> MedicalDevices => Set<MedicalDevice>();
public DbSet<SoftwareUpdate> SoftwareUpdates => Set<SoftwareUpdate>();
public DbSet<MedicalSupplier> MedicalSuppliers => Set<MedicalSupplier>();
public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        // HealthSystem has one or more Facilities of type Facility
        modelBuilder.Entity<Facility>()
            .HasOne<HealthSystem>()
            .WithMany(parent => parent.Facilities)
            .HasForeignKey("FacilitiesId");

        // HealthSystem has one or more Suppliers of type MedicalSupplier
        modelBuilder.Entity<MedicalSupplier>()
            .HasOne<HealthSystem>()
            .WithMany(parent => parent.Suppliers)
            .HasForeignKey("SuppliersId");

        // Facility has one HealthSystem of type HealthSystem
        modelBuilder.Entity<Facility>()
            .HasOne(x => x.HealthSystem)
            .WithMany()
            .HasForeignKey("HealthSystemId");


        // Facility has one or more Departments of type Department
        modelBuilder.Entity<Department>()
            .HasOne<Facility>()
            .WithMany(parent => parent.Departments)
            .HasForeignKey("DepartmentsId");

        // Facility has one or more CareTeams of type CareTeam
        modelBuilder.Entity<CareTeam>()
            .HasOne<Facility>()
            .WithMany(parent => parent.CareTeams)
            .HasForeignKey("CareTeamsId");

        // Facility has one or more Laboratories of type Laboratory
        modelBuilder.Entity<Laboratory>()
            .HasOne<Facility>()
            .WithMany(parent => parent.Laboratories)
            .HasForeignKey("LaboratoriesId");

        // Facility has one or more ImagingCenters of type ImagingCenter
        modelBuilder.Entity<ImagingCenter>()
            .HasOne<Facility>()
            .WithMany(parent => parent.ImagingCenters)
            .HasForeignKey("ImagingCentersId");

        // Facility has one or more Pharmacies of type Pharmacy
        modelBuilder.Entity<Pharmacy>()
            .HasOne<Facility>()
            .WithMany(parent => parent.Pharmacies)
            .HasForeignKey("PharmaciesId");

        // Facility has one or more InventoryItems of type InventoryItem
        modelBuilder.Entity<InventoryItem>()
            .HasOne<Facility>()
            .WithMany(parent => parent.InventoryItems)
            .HasForeignKey("InventoryItemsId");

        // Department has one Facility of type Facility
        modelBuilder.Entity<Department>()
            .HasOne(x => x.Facility)
            .WithMany()
            .HasForeignKey("FacilityId");


        // Department has one or more CareTeams of type CareTeam
        modelBuilder.Entity<CareTeam>()
            .HasOne<Department>()
            .WithMany(parent => parent.CareTeams)
            .HasForeignKey("CareTeamsId");

        // CareTeam has one Department of type Department
        modelBuilder.Entity<CareTeam>()
            .HasOne(x => x.Department)
            .WithMany()
            .HasForeignKey("DepartmentId");


        // CareTeam has one or more Clinicians of type Clinician
        modelBuilder.Entity<Clinician>()
            .HasOne<CareTeam>()
            .WithMany(parent => parent.Clinicians)
            .HasForeignKey("CliniciansId");

        // CareTeam has one or more Patients of type Patient
        modelBuilder.Entity<Patient>()
            .HasOne<CareTeam>()
            .WithMany(parent => parent.Patients)
            .HasForeignKey("PatientsId");


        // Clinician has one or more CareTeams of type CareTeam
        modelBuilder.Entity<CareTeam>()
            .HasOne<Clinician>()
            .WithMany(parent => parent.CareTeams)
            .HasForeignKey("CareTeamsId");

        // Clinician has one or more Appointments of type Appointment
        modelBuilder.Entity<Appointment>()
            .HasOne<Clinician>()
            .WithMany(parent => parent.Appointments)
            .HasForeignKey("AppointmentsId");

        // Clinician has one or more Encounters of type Encounter
        modelBuilder.Entity<Encounter>()
            .HasOne<Clinician>()
            .WithMany(parent => parent.Encounters)
            .HasForeignKey("EncountersId");

        // Clinician has one or more Procedures of type Procedure
        modelBuilder.Entity<Procedure>()
            .HasOne<Clinician>()
            .WithMany(parent => parent.Procedures)
            .HasForeignKey("ProceduresId");

        // Clinician has one or more ImagingReports of type ImagingReport
        modelBuilder.Entity<ImagingReport>()
            .HasOne<Clinician>()
            .WithMany(parent => parent.ImagingReports)
            .HasForeignKey("ImagingReportsId");


        // Patient has one or more Appointments of type Appointment
        modelBuilder.Entity<Appointment>()
            .HasOne<Patient>()
            .WithMany(parent => parent.Appointments)
            .HasForeignKey("AppointmentsId");

        // Patient has one or more Encounters of type Encounter
        modelBuilder.Entity<Encounter>()
            .HasOne<Patient>()
            .WithMany(parent => parent.Encounters)
            .HasForeignKey("EncountersId");

        // Patient has one or more CarePlans of type CarePlan
        modelBuilder.Entity<CarePlan>()
            .HasOne<Patient>()
            .WithMany(parent => parent.CarePlans)
            .HasForeignKey("CarePlansId");

        // Patient has one or more Allergies of type Allergy
        modelBuilder.Entity<Allergy>()
            .HasOne<Patient>()
            .WithMany(parent => parent.Allergies)
            .HasForeignKey("AllergiesId");

        // Patient has one or more Conditions of type Condition
        modelBuilder.Entity<Condition>()
            .HasOne<Patient>()
            .WithMany(parent => parent.Conditions)
            .HasForeignKey("ConditionsId");

        // Patient has one or more MedicationOrders of type MedicationOrder
        modelBuilder.Entity<MedicationOrder>()
            .HasOne<Patient>()
            .WithMany(parent => parent.MedicationOrders)
            .HasForeignKey("MedicationOrdersId");

        // Patient has one or more LabOrders of type LaboratoryOrder
        modelBuilder.Entity<LaboratoryOrder>()
            .HasOne<Patient>()
            .WithMany(parent => parent.LabOrders)
            .HasForeignKey("LabOrdersId");

        // Patient has one or more ImagingOrders of type ImagingOrder
        modelBuilder.Entity<ImagingOrder>()
            .HasOne<Patient>()
            .WithMany(parent => parent.ImagingOrders)
            .HasForeignKey("ImagingOrdersId");

        // Patient has one or more Coverages of type Coverage
        modelBuilder.Entity<Coverage>()
            .HasOne<Patient>()
            .WithMany(parent => parent.Coverages)
            .HasForeignKey("CoveragesId");

        // Patient has one or more Claims of type Claim
        modelBuilder.Entity<Claim>()
            .HasOne<Patient>()
            .WithMany(parent => parent.Claims)
            .HasForeignKey("ClaimsId");

        // Patient has one or more Devices of type MedicalDevice
        modelBuilder.Entity<MedicalDevice>()
            .HasOne<Patient>()
            .WithMany(parent => parent.Devices)
            .HasForeignKey("DevicesId");

        // Patient has one or more Observations of type Observation
        modelBuilder.Entity<Observation>()
            .HasOne<Patient>()
            .WithMany(parent => parent.Observations)
            .HasForeignKey("ObservationsId");

        // Appointment has one Patient of type Patient
        modelBuilder.Entity<Appointment>()
            .HasOne(x => x.Patient)
            .WithMany()
            .HasForeignKey("PatientId");

        // Appointment has one Clinician of type Clinician
        modelBuilder.Entity<Appointment>()
            .HasOne(x => x.Clinician)
            .WithMany()
            .HasForeignKey("ClinicianId");

        // Appointment has one Facility of type Facility
        modelBuilder.Entity<Appointment>()
            .HasOne(x => x.Facility)
            .WithMany()
            .HasForeignKey("FacilityId");

        // Appointment has one Encounter of type Encounter
        modelBuilder.Entity<Appointment>()
            .HasOne(x => x.Encounter)
            .WithMany()
            .HasForeignKey("EncounterId");


        // Encounter has one Patient of type Patient
        modelBuilder.Entity<Encounter>()
            .HasOne(x => x.Patient)
            .WithMany()
            .HasForeignKey("PatientId");

        // Encounter has one Clinician of type Clinician
        modelBuilder.Entity<Encounter>()
            .HasOne(x => x.Clinician)
            .WithMany()
            .HasForeignKey("ClinicianId");

        // Encounter has one Facility of type Facility
        modelBuilder.Entity<Encounter>()
            .HasOne(x => x.Facility)
            .WithMany()
            .HasForeignKey("FacilityId");

        // Encounter has one Appointment of type Appointment
        modelBuilder.Entity<Encounter>()
            .HasOne(x => x.Appointment)
            .WithMany()
            .HasForeignKey("AppointmentId");

        // Encounter has one Admission of type Admission
        modelBuilder.Entity<Encounter>()
            .HasOne(x => x.Admission)
            .WithMany()
            .HasForeignKey("AdmissionId");

        // Encounter has one Discharge of type Discharge
        modelBuilder.Entity<Encounter>()
            .HasOne(x => x.Discharge)
            .WithMany()
            .HasForeignKey("DischargeId");


        // Encounter has one or more Diagnoses of type Diagnosis
        modelBuilder.Entity<Diagnosis>()
            .HasOne<Encounter>()
            .WithMany(parent => parent.Diagnoses)
            .HasForeignKey("DiagnosesId");

        // Encounter has one or more Procedures of type Procedure
        modelBuilder.Entity<Procedure>()
            .HasOne<Encounter>()
            .WithMany(parent => parent.Procedures)
            .HasForeignKey("ProceduresId");

        // Encounter has one or more Observations of type Observation
        modelBuilder.Entity<Observation>()
            .HasOne<Encounter>()
            .WithMany(parent => parent.Observations)
            .HasForeignKey("ObservationsId");

        // Encounter has one or more Orders of type ClinicalOrder
        modelBuilder.Entity<ClinicalOrder>()
            .HasOne<Encounter>()
            .WithMany(parent => parent.Orders)
            .HasForeignKey("OrdersId");

        // Admission has one Encounter of type Encounter
        modelBuilder.Entity<Admission>()
            .HasOne(x => x.Encounter)
            .WithMany()
            .HasForeignKey("EncounterId");

        // Admission has one Facility of type Facility
        modelBuilder.Entity<Admission>()
            .HasOne(x => x.Facility)
            .WithMany()
            .HasForeignKey("FacilityId");


        // Discharge has one Encounter of type Encounter
        modelBuilder.Entity<Discharge>()
            .HasOne(x => x.Encounter)
            .WithMany()
            .HasForeignKey("EncounterId");


        // ClinicalOrder has one Patient of type Patient
        modelBuilder.Entity<ClinicalOrder>()
            .HasOne(x => x.Patient)
            .WithMany()
            .HasForeignKey("PatientId");

        // ClinicalOrder has one Encounter of type Encounter
        modelBuilder.Entity<ClinicalOrder>()
            .HasOne(x => x.Encounter)
            .WithMany()
            .HasForeignKey("EncounterId");

        // ClinicalOrder has one OrderingClinician of type Clinician
        modelBuilder.Entity<ClinicalOrder>()
            .HasOne(x => x.OrderingClinician)
            .WithMany()
            .HasForeignKey("OrderingClinicianId");


        // ClinicalOrder has one or more MedicationOrders of type MedicationOrder
        modelBuilder.Entity<MedicationOrder>()
            .HasOne<ClinicalOrder>()
            .WithMany(parent => parent.MedicationOrders)
            .HasForeignKey("MedicationOrdersId");

        // ClinicalOrder has one or more LaboratoryOrders of type LaboratoryOrder
        modelBuilder.Entity<LaboratoryOrder>()
            .HasOne<ClinicalOrder>()
            .WithMany(parent => parent.LaboratoryOrders)
            .HasForeignKey("LaboratoryOrdersId");

        // ClinicalOrder has one or more ImagingOrders of type ImagingOrder
        modelBuilder.Entity<ImagingOrder>()
            .HasOne<ClinicalOrder>()
            .WithMany(parent => parent.ImagingOrders)
            .HasForeignKey("ImagingOrdersId");

        // ClinicalOrder has one or more ProcedureOrders of type ProcedureOrder
        modelBuilder.Entity<ProcedureOrder>()
            .HasOne<ClinicalOrder>()
            .WithMany(parent => parent.ProcedureOrders)
            .HasForeignKey("ProcedureOrdersId");

        // ClinicalOrder has one or more Authorizations of type Authorization
        modelBuilder.Entity<Authorization>()
            .HasOne<ClinicalOrder>()
            .WithMany(parent => parent.Authorizations)
            .HasForeignKey("AuthorizationsId");

        // MedicationOrder has one Order of type ClinicalOrder
        modelBuilder.Entity<MedicationOrder>()
            .HasOne(x => x.Order)
            .WithMany()
            .HasForeignKey("OrderId");

        // MedicationOrder has one Pharmacy of type Pharmacy
        modelBuilder.Entity<MedicationOrder>()
            .HasOne(x => x.Pharmacy)
            .WithMany()
            .HasForeignKey("PharmacyId");


        // MedicationOrder has one or more Dispenses of type MedicationDispense
        modelBuilder.Entity<MedicationDispense>()
            .HasOne<MedicationOrder>()
            .WithMany(parent => parent.Dispenses)
            .HasForeignKey("DispensesId");

        // Laboratory has one Facility of type Facility
        modelBuilder.Entity<Laboratory>()
            .HasOne(x => x.Facility)
            .WithMany()
            .HasForeignKey("FacilityId");


        // Laboratory has one or more LaboratoryOrders of type LaboratoryOrder
        modelBuilder.Entity<LaboratoryOrder>()
            .HasOne<Laboratory>()
            .WithMany(parent => parent.LaboratoryOrders)
            .HasForeignKey("LaboratoryOrdersId");

        // Laboratory has one or more LabResults of type LabResult
        modelBuilder.Entity<LabResult>()
            .HasOne<Laboratory>()
            .WithMany(parent => parent.LabResults)
            .HasForeignKey("LabResultsId");

        // LaboratoryOrder has one Order of type ClinicalOrder
        modelBuilder.Entity<LaboratoryOrder>()
            .HasOne(x => x.Order)
            .WithMany()
            .HasForeignKey("OrderId");

        // LaboratoryOrder has one Laboratory of type Laboratory
        modelBuilder.Entity<LaboratoryOrder>()
            .HasOne(x => x.Laboratory)
            .WithMany()
            .HasForeignKey("LaboratoryId");


        // LaboratoryOrder has one or more Results of type LabResult
        modelBuilder.Entity<LabResult>()
            .HasOne<LaboratoryOrder>()
            .WithMany(parent => parent.Results)
            .HasForeignKey("ResultsId");

        // LabResult has one LaboratoryOrder of type LaboratoryOrder
        modelBuilder.Entity<LabResult>()
            .HasOne(x => x.LaboratoryOrder)
            .WithMany()
            .HasForeignKey("LaboratoryOrderId");

        // LabResult has one Laboratory of type Laboratory
        modelBuilder.Entity<LabResult>()
            .HasOne(x => x.Laboratory)
            .WithMany()
            .HasForeignKey("LaboratoryId");


        // LabResult has one or more Observations of type Observation
        modelBuilder.Entity<Observation>()
            .HasOne<LabResult>()
            .WithMany(parent => parent.Observations)
            .HasForeignKey("ObservationsId");

        // ImagingCenter has one Facility of type Facility
        modelBuilder.Entity<ImagingCenter>()
            .HasOne(x => x.Facility)
            .WithMany()
            .HasForeignKey("FacilityId");


        // ImagingCenter has one or more ImagingOrders of type ImagingOrder
        modelBuilder.Entity<ImagingOrder>()
            .HasOne<ImagingCenter>()
            .WithMany(parent => parent.ImagingOrders)
            .HasForeignKey("ImagingOrdersId");

        // ImagingCenter has one or more ImagingReports of type ImagingReport
        modelBuilder.Entity<ImagingReport>()
            .HasOne<ImagingCenter>()
            .WithMany(parent => parent.ImagingReports)
            .HasForeignKey("ImagingReportsId");

        // ImagingOrder has one Order of type ClinicalOrder
        modelBuilder.Entity<ImagingOrder>()
            .HasOne(x => x.Order)
            .WithMany()
            .HasForeignKey("OrderId");

        // ImagingOrder has one ImagingCenter of type ImagingCenter
        modelBuilder.Entity<ImagingOrder>()
            .HasOne(x => x.ImagingCenter)
            .WithMany()
            .HasForeignKey("ImagingCenterId");


        // ImagingOrder has one or more Reports of type ImagingReport
        modelBuilder.Entity<ImagingReport>()
            .HasOne<ImagingOrder>()
            .WithMany(parent => parent.Reports)
            .HasForeignKey("ReportsId");

        // ImagingReport has one ImagingOrder of type ImagingOrder
        modelBuilder.Entity<ImagingReport>()
            .HasOne(x => x.ImagingOrder)
            .WithMany()
            .HasForeignKey("ImagingOrderId");

        // ImagingReport has one Clinician of type Clinician
        modelBuilder.Entity<ImagingReport>()
            .HasOne(x => x.Clinician)
            .WithMany()
            .HasForeignKey("ClinicianId");

        // ImagingReport has one Encounter of type Encounter
        modelBuilder.Entity<ImagingReport>()
            .HasOne(x => x.Encounter)
            .WithMany()
            .HasForeignKey("EncounterId");

        // ImagingReport has one ImagingCenter of type ImagingCenter
        modelBuilder.Entity<ImagingReport>()
            .HasOne(x => x.ImagingCenter)
            .WithMany()
            .HasForeignKey("ImagingCenterId");


        // ProcedureOrder has one Order of type ClinicalOrder
        modelBuilder.Entity<ProcedureOrder>()
            .HasOne(x => x.Order)
            .WithMany()
            .HasForeignKey("OrderId");

        // ProcedureOrder has one Facility of type Facility
        modelBuilder.Entity<ProcedureOrder>()
            .HasOne(x => x.Facility)
            .WithMany()
            .HasForeignKey("FacilityId");

        // ProcedureOrder has one Procedure of type Procedure
        modelBuilder.Entity<ProcedureOrder>()
            .HasOne(x => x.Procedure)
            .WithMany()
            .HasForeignKey("ProcedureId");


        // Procedure has one Encounter of type Encounter
        modelBuilder.Entity<Procedure>()
            .HasOne(x => x.Encounter)
            .WithMany()
            .HasForeignKey("EncounterId");

        // Procedure has one Performer of type Clinician
        modelBuilder.Entity<Procedure>()
            .HasOne(x => x.Performer)
            .WithMany()
            .HasForeignKey("PerformerId");

        // Procedure has one ProcedureOrder of type ProcedureOrder
        modelBuilder.Entity<Procedure>()
            .HasOne(x => x.ProcedureOrder)
            .WithMany()
            .HasForeignKey("ProcedureOrderId");


        // Pharmacy has one Facility of type Facility
        modelBuilder.Entity<Pharmacy>()
            .HasOne(x => x.Facility)
            .WithMany()
            .HasForeignKey("FacilityId");


        // Pharmacy has one or more MedicationDispenses of type MedicationDispense
        modelBuilder.Entity<MedicationDispense>()
            .HasOne<Pharmacy>()
            .WithMany(parent => parent.MedicationDispenses)
            .HasForeignKey("MedicationDispensesId");

        // Pharmacy has one or more MedicationOrders of type MedicationOrder
        modelBuilder.Entity<MedicationOrder>()
            .HasOne<Pharmacy>()
            .WithMany(parent => parent.MedicationOrders)
            .HasForeignKey("MedicationOrdersId");

        // MedicationDispense has one MedicationOrder of type MedicationOrder
        modelBuilder.Entity<MedicationDispense>()
            .HasOne(x => x.MedicationOrder)
            .WithMany()
            .HasForeignKey("MedicationOrderId");

        // MedicationDispense has one Pharmacy of type Pharmacy
        modelBuilder.Entity<MedicationDispense>()
            .HasOne(x => x.Pharmacy)
            .WithMany()
            .HasForeignKey("PharmacyId");

        // MedicationDispense has one Patient of type Patient
        modelBuilder.Entity<MedicationDispense>()
            .HasOne(x => x.Patient)
            .WithMany()
            .HasForeignKey("PatientId");


        // Diagnosis has one Encounter of type Encounter
        modelBuilder.Entity<Diagnosis>()
            .HasOne(x => x.Encounter)
            .WithMany()
            .HasForeignKey("EncounterId");

        // Diagnosis has one Patient of type Patient
        modelBuilder.Entity<Diagnosis>()
            .HasOne(x => x.Patient)
            .WithMany()
            .HasForeignKey("PatientId");


        // Observation has one Encounter of type Encounter
        modelBuilder.Entity<Observation>()
            .HasOne(x => x.Encounter)
            .WithMany()
            .HasForeignKey("EncounterId");

        // Observation has one Patient of type Patient
        modelBuilder.Entity<Observation>()
            .HasOne(x => x.Patient)
            .WithMany()
            .HasForeignKey("PatientId");

        // Observation has one Device of type MedicalDevice
        modelBuilder.Entity<Observation>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("DeviceId");

        // Observation has one LabResult of type LabResult
        modelBuilder.Entity<Observation>()
            .HasOne(x => x.LabResult)
            .WithMany()
            .HasForeignKey("LabResultId");


        // CarePlan has one Patient of type Patient
        modelBuilder.Entity<CarePlan>()
            .HasOne(x => x.Patient)
            .WithMany()
            .HasForeignKey("PatientId");

        // CarePlan has one CareTeam of type CareTeam
        modelBuilder.Entity<CarePlan>()
            .HasOne(x => x.CareTeam)
            .WithMany()
            .HasForeignKey("CareTeamId");


        // CarePlan has one or more Encounters of type Encounter
        modelBuilder.Entity<Encounter>()
            .HasOne<CarePlan>()
            .WithMany(parent => parent.Encounters)
            .HasForeignKey("EncountersId");

        // CarePlan has one or more Tasks of type CareTask
        modelBuilder.Entity<CareTask>()
            .HasOne<CarePlan>()
            .WithMany(parent => parent.Tasks)
            .HasForeignKey("TasksId");

        // CareTask has one CarePlan of type CarePlan
        modelBuilder.Entity<CareTask>()
            .HasOne(x => x.CarePlan)
            .WithMany()
            .HasForeignKey("CarePlanId");

        // CareTask has one AssignedTo of type Clinician
        modelBuilder.Entity<CareTask>()
            .HasOne(x => x.AssignedTo)
            .WithMany()
            .HasForeignKey("AssignedToId");

        // CareTask has one Encounter of type Encounter
        modelBuilder.Entity<CareTask>()
            .HasOne(x => x.Encounter)
            .WithMany()
            .HasForeignKey("EncounterId");


        // Allergy has one Patient of type Patient
        modelBuilder.Entity<Allergy>()
            .HasOne(x => x.Patient)
            .WithMany()
            .HasForeignKey("PatientId");


        // Condition has one Patient of type Patient
        modelBuilder.Entity<Condition>()
            .HasOne(x => x.Patient)
            .WithMany()
            .HasForeignKey("PatientId");



        // InsurancePayer has one or more Plans of type InsurancePlan
        modelBuilder.Entity<InsurancePlan>()
            .HasOne<InsurancePayer>()
            .WithMany(parent => parent.Plans)
            .HasForeignKey("PlansId");

        // InsurancePayer has one or more Claims of type Claim
        modelBuilder.Entity<Claim>()
            .HasOne<InsurancePayer>()
            .WithMany(parent => parent.Claims)
            .HasForeignKey("ClaimsId");

        // InsurancePlan has one Payer of type InsurancePayer
        modelBuilder.Entity<InsurancePlan>()
            .HasOne(x => x.Payer)
            .WithMany()
            .HasForeignKey("PayerId");


        // InsurancePlan has one or more Coverages of type Coverage
        modelBuilder.Entity<Coverage>()
            .HasOne<InsurancePlan>()
            .WithMany(parent => parent.Coverages)
            .HasForeignKey("CoveragesId");

        // Coverage has one Patient of type Patient
        modelBuilder.Entity<Coverage>()
            .HasOne(x => x.Patient)
            .WithMany()
            .HasForeignKey("PatientId");

        // Coverage has one Plan of type InsurancePlan
        modelBuilder.Entity<Coverage>()
            .HasOne(x => x.Plan)
            .WithMany()
            .HasForeignKey("PlanId");


        // Coverage has one or more Claims of type Claim
        modelBuilder.Entity<Claim>()
            .HasOne<Coverage>()
            .WithMany(parent => parent.Claims)
            .HasForeignKey("ClaimsId");

        // Coverage has one or more Authorizations of type Authorization
        modelBuilder.Entity<Authorization>()
            .HasOne<Coverage>()
            .WithMany(parent => parent.Authorizations)
            .HasForeignKey("AuthorizationsId");

        // Claim has one Patient of type Patient
        modelBuilder.Entity<Claim>()
            .HasOne(x => x.Patient)
            .WithMany()
            .HasForeignKey("PatientId");

        // Claim has one Coverage of type Coverage
        modelBuilder.Entity<Claim>()
            .HasOne(x => x.Coverage)
            .WithMany()
            .HasForeignKey("CoverageId");

        // Claim has one Encounter of type Encounter
        modelBuilder.Entity<Claim>()
            .HasOne(x => x.Encounter)
            .WithMany()
            .HasForeignKey("EncounterId");

        // Claim has one Payer of type InsurancePayer
        modelBuilder.Entity<Claim>()
            .HasOne(x => x.Payer)
            .WithMany()
            .HasForeignKey("PayerId");


        // Claim has one or more Invoices of type Invoice
        modelBuilder.Entity<Invoice>()
            .HasOne<Claim>()
            .WithMany(parent => parent.Invoices)
            .HasForeignKey("InvoicesId");

        // Authorization has one Coverage of type Coverage
        modelBuilder.Entity<Authorization>()
            .HasOne(x => x.Coverage)
            .WithMany()
            .HasForeignKey("CoverageId");

        // Authorization has one Order of type ClinicalOrder
        modelBuilder.Entity<Authorization>()
            .HasOne(x => x.Order)
            .WithMany()
            .HasForeignKey("OrderId");


        // Invoice has one Patient of type Patient
        modelBuilder.Entity<Invoice>()
            .HasOne(x => x.Patient)
            .WithMany()
            .HasForeignKey("PatientId");

        // Invoice has one Claim of type Claim
        modelBuilder.Entity<Invoice>()
            .HasOne(x => x.Claim)
            .WithMany()
            .HasForeignKey("ClaimId");


        // Invoice has one or more Payments of type Payment
        modelBuilder.Entity<Payment>()
            .HasOne<Invoice>()
            .WithMany(parent => parent.Payments)
            .HasForeignKey("PaymentsId");

        // Payment has one Invoice of type Invoice
        modelBuilder.Entity<Payment>()
            .HasOne(x => x.Invoice)
            .WithMany()
            .HasForeignKey("InvoiceId");

        // Payment has one Payer of type InsurancePayer
        modelBuilder.Entity<Payment>()
            .HasOne(x => x.Payer)
            .WithMany()
            .HasForeignKey("PayerId");


        // MedicalDevice has one Patient of type Patient
        modelBuilder.Entity<MedicalDevice>()
            .HasOne(x => x.Patient)
            .WithMany()
            .HasForeignKey("PatientId");


        // MedicalDevice has one or more Observations of type Observation
        modelBuilder.Entity<Observation>()
            .HasOne<MedicalDevice>()
            .WithMany(parent => parent.Observations)
            .HasForeignKey("ObservationsId");

        // MedicalDevice has one or more SoftwareUpdates of type SoftwareUpdate
        modelBuilder.Entity<SoftwareUpdate>()
            .HasOne<MedicalDevice>()
            .WithMany(parent => parent.SoftwareUpdates)
            .HasForeignKey("SoftwareUpdatesId");

        // SoftwareUpdate has one Device of type MedicalDevice
        modelBuilder.Entity<SoftwareUpdate>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("DeviceId");



        // MedicalSupplier has one or more Facilities of type Facility
        modelBuilder.Entity<Facility>()
            .HasOne<MedicalSupplier>()
            .WithMany(parent => parent.Facilities)
            .HasForeignKey("FacilitiesId");

        // MedicalSupplier has one or more InventoryItems of type InventoryItem
        modelBuilder.Entity<InventoryItem>()
            .HasOne<MedicalSupplier>()
            .WithMany(parent => parent.InventoryItems)
            .HasForeignKey("InventoryItemsId");

        // InventoryItem has one Facility of type Facility
        modelBuilder.Entity<InventoryItem>()
            .HasOne(x => x.Facility)
            .WithMany()
            .HasForeignKey("FacilityId");

        // InventoryItem has one Supplier of type MedicalSupplier
        modelBuilder.Entity<InventoryItem>()
            .HasOne(x => x.Supplier)
            .WithMany()
            .HasForeignKey("SupplierId");


    }
}
