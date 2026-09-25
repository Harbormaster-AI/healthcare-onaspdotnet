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
            .HasForeignKey("HealthSystem_Id");

        // HealthSystem has one or more Suppliers of type MedicalSupplier
        modelBuilder.Entity<MedicalSupplier>()
            .HasOne<HealthSystem>()
            .WithMany(parent => parent.Suppliers)
            .HasForeignKey("HealthSystem_Id");

        // Facility has one HealthSystem of type HealthSystem
        modelBuilder.Entity<Facility>()
            .HasOne(x => x.HealthSystem)
            .WithMany()
            .HasForeignKey("HealthSystem_Id");


        // Facility has one or more Departments of type Department
        modelBuilder.Entity<Department>()
            .HasOne<Facility>()
            .WithMany(parent => parent.Departments)
            .HasForeignKey("Facility_Id");

        // Facility has one or more CareTeams of type CareTeam
        modelBuilder.Entity<CareTeam>()
            .HasOne<Facility>()
            .WithMany(parent => parent.CareTeams)
            .HasForeignKey("Facility_Id");

        // Facility has one or more Laboratories of type Laboratory
        modelBuilder.Entity<Laboratory>()
            .HasOne<Facility>()
            .WithMany(parent => parent.Laboratories)
            .HasForeignKey("Facility_Id");

        // Facility has one or more ImagingCenters of type ImagingCenter
        modelBuilder.Entity<ImagingCenter>()
            .HasOne<Facility>()
            .WithMany(parent => parent.ImagingCenters)
            .HasForeignKey("Facility_Id");

        // Facility has one or more Pharmacies of type Pharmacy
        modelBuilder.Entity<Pharmacy>()
            .HasOne<Facility>()
            .WithMany(parent => parent.Pharmacies)
            .HasForeignKey("Facility_Id");

        // Facility has one or more InventoryItems of type InventoryItem
        modelBuilder.Entity<InventoryItem>()
            .HasOne<Facility>()
            .WithMany(parent => parent.InventoryItems)
            .HasForeignKey("Facility_Id");

        // Department has one Facility of type Facility
        modelBuilder.Entity<Department>()
            .HasOne(x => x.Facility)
            .WithMany()
            .HasForeignKey("Facility_Id");


        // Department has one or more CareTeams of type CareTeam
        modelBuilder.Entity<CareTeam>()
            .HasOne<Department>()
            .WithMany(parent => parent.CareTeams)
            .HasForeignKey("Department_Id");

        // CareTeam has one Department of type Department
        modelBuilder.Entity<CareTeam>()
            .HasOne(x => x.Department)
            .WithMany()
            .HasForeignKey("Department_Id");


        // CareTeam has one or more Clinicians of type Clinician
        modelBuilder.Entity<Clinician>()
            .HasOne<CareTeam>()
            .WithMany(parent => parent.Clinicians)
            .HasForeignKey("CareTeam_Id");

        // CareTeam has one or more Patients of type Patient
        modelBuilder.Entity<Patient>()
            .HasOne<CareTeam>()
            .WithMany(parent => parent.Patients)
            .HasForeignKey("CareTeam_Id");


        // Clinician has one or more CareTeams of type CareTeam
        modelBuilder.Entity<CareTeam>()
            .HasOne<Clinician>()
            .WithMany(parent => parent.CareTeams)
            .HasForeignKey("Clinician_Id");

        // Clinician has one or more Appointments of type Appointment
        modelBuilder.Entity<Appointment>()
            .HasOne<Clinician>()
            .WithMany(parent => parent.Appointments)
            .HasForeignKey("Clinician_Id");

        // Clinician has one or more Encounters of type Encounter
        modelBuilder.Entity<Encounter>()
            .HasOne<Clinician>()
            .WithMany(parent => parent.Encounters)
            .HasForeignKey("Clinician_Id");

        // Clinician has one or more Procedures of type Procedure
        modelBuilder.Entity<Procedure>()
            .HasOne<Clinician>()
            .WithMany(parent => parent.Procedures)
            .HasForeignKey("Clinician_Id");

        // Clinician has one or more ImagingReports of type ImagingReport
        modelBuilder.Entity<ImagingReport>()
            .HasOne<Clinician>()
            .WithMany(parent => parent.ImagingReports)
            .HasForeignKey("Clinician_Id");


        // Patient has one or more Appointments of type Appointment
        modelBuilder.Entity<Appointment>()
            .HasOne<Patient>()
            .WithMany(parent => parent.Appointments)
            .HasForeignKey("Patient_Id");

        // Patient has one or more Encounters of type Encounter
        modelBuilder.Entity<Encounter>()
            .HasOne<Patient>()
            .WithMany(parent => parent.Encounters)
            .HasForeignKey("Patient_Id");

        // Patient has one or more CarePlans of type CarePlan
        modelBuilder.Entity<CarePlan>()
            .HasOne<Patient>()
            .WithMany(parent => parent.CarePlans)
            .HasForeignKey("Patient_Id");

        // Patient has one or more Allergies of type Allergy
        modelBuilder.Entity<Allergy>()
            .HasOne<Patient>()
            .WithMany(parent => parent.Allergies)
            .HasForeignKey("Patient_Id");

        // Patient has one or more Conditions of type Condition
        modelBuilder.Entity<Condition>()
            .HasOne<Patient>()
            .WithMany(parent => parent.Conditions)
            .HasForeignKey("Patient_Id");

        // Patient has one or more MedicationOrders of type MedicationOrder
        modelBuilder.Entity<MedicationOrder>()
            .HasOne<Patient>()
            .WithMany(parent => parent.MedicationOrders)
            .HasForeignKey("Patient_Id");

        // Patient has one or more LabOrders of type LaboratoryOrder
        modelBuilder.Entity<LaboratoryOrder>()
            .HasOne<Patient>()
            .WithMany(parent => parent.LabOrders)
            .HasForeignKey("Patient_Id");

        // Patient has one or more ImagingOrders of type ImagingOrder
        modelBuilder.Entity<ImagingOrder>()
            .HasOne<Patient>()
            .WithMany(parent => parent.ImagingOrders)
            .HasForeignKey("Patient_Id");

        // Patient has one or more Coverages of type Coverage
        modelBuilder.Entity<Coverage>()
            .HasOne<Patient>()
            .WithMany(parent => parent.Coverages)
            .HasForeignKey("Patient_Id");

        // Patient has one or more Claims of type Claim
        modelBuilder.Entity<Claim>()
            .HasOne<Patient>()
            .WithMany(parent => parent.Claims)
            .HasForeignKey("Patient_Id");

        // Patient has one or more Devices of type MedicalDevice
        modelBuilder.Entity<MedicalDevice>()
            .HasOne<Patient>()
            .WithMany(parent => parent.Devices)
            .HasForeignKey("Patient_Id");

        // Patient has one or more Observations of type Observation
        modelBuilder.Entity<Observation>()
            .HasOne<Patient>()
            .WithMany(parent => parent.Observations)
            .HasForeignKey("Patient_Id");

        // Appointment has one Patient of type Patient
        modelBuilder.Entity<Appointment>()
            .HasOne(x => x.Patient)
            .WithMany()
            .HasForeignKey("Patient_Id");

        // Appointment has one Clinician of type Clinician
        modelBuilder.Entity<Appointment>()
            .HasOne(x => x.Clinician)
            .WithMany()
            .HasForeignKey("Clinician_Id");

        // Appointment has one Facility of type Facility
        modelBuilder.Entity<Appointment>()
            .HasOne(x => x.Facility)
            .WithMany()
            .HasForeignKey("Facility_Id");

        // Appointment has one Encounter of type Encounter
        modelBuilder.Entity<Appointment>()
            .HasOne(x => x.Encounter)
            .WithMany()
            .HasForeignKey("Encounter_Id");


        // Encounter has one Patient of type Patient
        modelBuilder.Entity<Encounter>()
            .HasOne(x => x.Patient)
            .WithMany()
            .HasForeignKey("Patient_Id");

        // Encounter has one Clinician of type Clinician
        modelBuilder.Entity<Encounter>()
            .HasOne(x => x.Clinician)
            .WithMany()
            .HasForeignKey("Clinician_Id");

        // Encounter has one Facility of type Facility
        modelBuilder.Entity<Encounter>()
            .HasOne(x => x.Facility)
            .WithMany()
            .HasForeignKey("Facility_Id");

        // Encounter has one Appointment of type Appointment
        modelBuilder.Entity<Encounter>()
            .HasOne(x => x.Appointment)
            .WithMany()
            .HasForeignKey("Appointment_Id");

        // Encounter has one Admission of type Admission
        modelBuilder.Entity<Encounter>()
            .HasOne(x => x.Admission)
            .WithMany()
            .HasForeignKey("Admission_Id");

        // Encounter has one Discharge of type Discharge
        modelBuilder.Entity<Encounter>()
            .HasOne(x => x.Discharge)
            .WithMany()
            .HasForeignKey("Discharge_Id");


        // Encounter has one or more Diagnoses of type Diagnosis
        modelBuilder.Entity<Diagnosis>()
            .HasOne<Encounter>()
            .WithMany(parent => parent.Diagnoses)
            .HasForeignKey("Encounter_Id");

        // Encounter has one or more Procedures of type Procedure
        modelBuilder.Entity<Procedure>()
            .HasOne<Encounter>()
            .WithMany(parent => parent.Procedures)
            .HasForeignKey("Encounter_Id");

        // Encounter has one or more Observations of type Observation
        modelBuilder.Entity<Observation>()
            .HasOne<Encounter>()
            .WithMany(parent => parent.Observations)
            .HasForeignKey("Encounter_Id");

        // Encounter has one or more Orders of type ClinicalOrder
        modelBuilder.Entity<ClinicalOrder>()
            .HasOne<Encounter>()
            .WithMany(parent => parent.Orders)
            .HasForeignKey("Encounter_Id");

        // Admission has one Encounter of type Encounter
        modelBuilder.Entity<Admission>()
            .HasOne(x => x.Encounter)
            .WithMany()
            .HasForeignKey("Encounter_Id");

        // Admission has one Facility of type Facility
        modelBuilder.Entity<Admission>()
            .HasOne(x => x.Facility)
            .WithMany()
            .HasForeignKey("Facility_Id");


        // Discharge has one Encounter of type Encounter
        modelBuilder.Entity<Discharge>()
            .HasOne(x => x.Encounter)
            .WithMany()
            .HasForeignKey("Encounter_Id");


        // ClinicalOrder has one Patient of type Patient
        modelBuilder.Entity<ClinicalOrder>()
            .HasOne(x => x.Patient)
            .WithMany()
            .HasForeignKey("Patient_Id");

        // ClinicalOrder has one Encounter of type Encounter
        modelBuilder.Entity<ClinicalOrder>()
            .HasOne(x => x.Encounter)
            .WithMany()
            .HasForeignKey("Encounter_Id");

        // ClinicalOrder has one OrderingClinician of type Clinician
        modelBuilder.Entity<ClinicalOrder>()
            .HasOne(x => x.OrderingClinician)
            .WithMany()
            .HasForeignKey("OrderingClinician_Id");


        // ClinicalOrder has one or more MedicationOrders of type MedicationOrder
        modelBuilder.Entity<MedicationOrder>()
            .HasOne<ClinicalOrder>()
            .WithMany(parent => parent.MedicationOrders)
            .HasForeignKey("ClinicalOrder_Id");

        // ClinicalOrder has one or more LaboratoryOrders of type LaboratoryOrder
        modelBuilder.Entity<LaboratoryOrder>()
            .HasOne<ClinicalOrder>()
            .WithMany(parent => parent.LaboratoryOrders)
            .HasForeignKey("ClinicalOrder_Id");

        // ClinicalOrder has one or more ImagingOrders of type ImagingOrder
        modelBuilder.Entity<ImagingOrder>()
            .HasOne<ClinicalOrder>()
            .WithMany(parent => parent.ImagingOrders)
            .HasForeignKey("ClinicalOrder_Id");

        // ClinicalOrder has one or more ProcedureOrders of type ProcedureOrder
        modelBuilder.Entity<ProcedureOrder>()
            .HasOne<ClinicalOrder>()
            .WithMany(parent => parent.ProcedureOrders)
            .HasForeignKey("ClinicalOrder_Id");

        // ClinicalOrder has one or more Authorizations of type Authorization
        modelBuilder.Entity<Authorization>()
            .HasOne<ClinicalOrder>()
            .WithMany(parent => parent.Authorizations)
            .HasForeignKey("ClinicalOrder_Id");

        // MedicationOrder has one Order of type ClinicalOrder
        modelBuilder.Entity<MedicationOrder>()
            .HasOne(x => x.Order)
            .WithMany()
            .HasForeignKey("Order_Id");

        // MedicationOrder has one Pharmacy of type Pharmacy
        modelBuilder.Entity<MedicationOrder>()
            .HasOne(x => x.Pharmacy)
            .WithMany()
            .HasForeignKey("Pharmacy_Id");


        // MedicationOrder has one or more Dispenses of type MedicationDispense
        modelBuilder.Entity<MedicationDispense>()
            .HasOne<MedicationOrder>()
            .WithMany(parent => parent.Dispenses)
            .HasForeignKey("MedicationOrder_Id");

        // Laboratory has one Facility of type Facility
        modelBuilder.Entity<Laboratory>()
            .HasOne(x => x.Facility)
            .WithMany()
            .HasForeignKey("Facility_Id");


        // Laboratory has one or more LaboratoryOrders of type LaboratoryOrder
        modelBuilder.Entity<LaboratoryOrder>()
            .HasOne<Laboratory>()
            .WithMany(parent => parent.LaboratoryOrders)
            .HasForeignKey("Laboratory_Id");

        // Laboratory has one or more LabResults of type LabResult
        modelBuilder.Entity<LabResult>()
            .HasOne<Laboratory>()
            .WithMany(parent => parent.LabResults)
            .HasForeignKey("Laboratory_Id");

        // LaboratoryOrder has one Order of type ClinicalOrder
        modelBuilder.Entity<LaboratoryOrder>()
            .HasOne(x => x.Order)
            .WithMany()
            .HasForeignKey("Order_Id");

        // LaboratoryOrder has one Laboratory of type Laboratory
        modelBuilder.Entity<LaboratoryOrder>()
            .HasOne(x => x.Laboratory)
            .WithMany()
            .HasForeignKey("Laboratory_Id");


        // LaboratoryOrder has one or more Results of type LabResult
        modelBuilder.Entity<LabResult>()
            .HasOne<LaboratoryOrder>()
            .WithMany(parent => parent.Results)
            .HasForeignKey("LaboratoryOrder_Id");

        // LabResult has one LaboratoryOrder of type LaboratoryOrder
        modelBuilder.Entity<LabResult>()
            .HasOne(x => x.LaboratoryOrder)
            .WithMany()
            .HasForeignKey("LaboratoryOrder_Id");

        // LabResult has one Laboratory of type Laboratory
        modelBuilder.Entity<LabResult>()
            .HasOne(x => x.Laboratory)
            .WithMany()
            .HasForeignKey("Laboratory_Id");


        // LabResult has one or more Observations of type Observation
        modelBuilder.Entity<Observation>()
            .HasOne<LabResult>()
            .WithMany(parent => parent.Observations)
            .HasForeignKey("LabResult_Id");

        // ImagingCenter has one Facility of type Facility
        modelBuilder.Entity<ImagingCenter>()
            .HasOne(x => x.Facility)
            .WithMany()
            .HasForeignKey("Facility_Id");


        // ImagingCenter has one or more ImagingOrders of type ImagingOrder
        modelBuilder.Entity<ImagingOrder>()
            .HasOne<ImagingCenter>()
            .WithMany(parent => parent.ImagingOrders)
            .HasForeignKey("ImagingCenter_Id");

        // ImagingCenter has one or more ImagingReports of type ImagingReport
        modelBuilder.Entity<ImagingReport>()
            .HasOne<ImagingCenter>()
            .WithMany(parent => parent.ImagingReports)
            .HasForeignKey("ImagingCenter_Id");

        // ImagingOrder has one Order of type ClinicalOrder
        modelBuilder.Entity<ImagingOrder>()
            .HasOne(x => x.Order)
            .WithMany()
            .HasForeignKey("Order_Id");

        // ImagingOrder has one ImagingCenter of type ImagingCenter
        modelBuilder.Entity<ImagingOrder>()
            .HasOne(x => x.ImagingCenter)
            .WithMany()
            .HasForeignKey("ImagingCenter_Id");


        // ImagingOrder has one or more Reports of type ImagingReport
        modelBuilder.Entity<ImagingReport>()
            .HasOne<ImagingOrder>()
            .WithMany(parent => parent.Reports)
            .HasForeignKey("ImagingOrder_Id");

        // ImagingReport has one ImagingOrder of type ImagingOrder
        modelBuilder.Entity<ImagingReport>()
            .HasOne(x => x.ImagingOrder)
            .WithMany()
            .HasForeignKey("ImagingOrder_Id");

        // ImagingReport has one Clinician of type Clinician
        modelBuilder.Entity<ImagingReport>()
            .HasOne(x => x.Clinician)
            .WithMany()
            .HasForeignKey("Clinician_Id");

        // ImagingReport has one Encounter of type Encounter
        modelBuilder.Entity<ImagingReport>()
            .HasOne(x => x.Encounter)
            .WithMany()
            .HasForeignKey("Encounter_Id");

        // ImagingReport has one ImagingCenter of type ImagingCenter
        modelBuilder.Entity<ImagingReport>()
            .HasOne(x => x.ImagingCenter)
            .WithMany()
            .HasForeignKey("ImagingCenter_Id");


        // ProcedureOrder has one Order of type ClinicalOrder
        modelBuilder.Entity<ProcedureOrder>()
            .HasOne(x => x.Order)
            .WithMany()
            .HasForeignKey("Order_Id");

        // ProcedureOrder has one Facility of type Facility
        modelBuilder.Entity<ProcedureOrder>()
            .HasOne(x => x.Facility)
            .WithMany()
            .HasForeignKey("Facility_Id");

        // ProcedureOrder has one Procedure of type Procedure
        modelBuilder.Entity<ProcedureOrder>()
            .HasOne(x => x.Procedure)
            .WithMany()
            .HasForeignKey("Procedure_Id");


        // Procedure has one Encounter of type Encounter
        modelBuilder.Entity<Procedure>()
            .HasOne(x => x.Encounter)
            .WithMany()
            .HasForeignKey("Encounter_Id");

        // Procedure has one Performer of type Clinician
        modelBuilder.Entity<Procedure>()
            .HasOne(x => x.Performer)
            .WithMany()
            .HasForeignKey("Performer_Id");

        // Procedure has one ProcedureOrder of type ProcedureOrder
        modelBuilder.Entity<Procedure>()
            .HasOne(x => x.ProcedureOrder)
            .WithMany()
            .HasForeignKey("ProcedureOrder_Id");


        // Pharmacy has one Facility of type Facility
        modelBuilder.Entity<Pharmacy>()
            .HasOne(x => x.Facility)
            .WithMany()
            .HasForeignKey("Facility_Id");


        // Pharmacy has one or more MedicationDispenses of type MedicationDispense
        modelBuilder.Entity<MedicationDispense>()
            .HasOne<Pharmacy>()
            .WithMany(parent => parent.MedicationDispenses)
            .HasForeignKey("Pharmacy_Id");

        // Pharmacy has one or more MedicationOrders of type MedicationOrder
        modelBuilder.Entity<MedicationOrder>()
            .HasOne<Pharmacy>()
            .WithMany(parent => parent.MedicationOrders)
            .HasForeignKey("Pharmacy_Id");

        // MedicationDispense has one MedicationOrder of type MedicationOrder
        modelBuilder.Entity<MedicationDispense>()
            .HasOne(x => x.MedicationOrder)
            .WithMany()
            .HasForeignKey("MedicationOrder_Id");

        // MedicationDispense has one Pharmacy of type Pharmacy
        modelBuilder.Entity<MedicationDispense>()
            .HasOne(x => x.Pharmacy)
            .WithMany()
            .HasForeignKey("Pharmacy_Id");

        // MedicationDispense has one Patient of type Patient
        modelBuilder.Entity<MedicationDispense>()
            .HasOne(x => x.Patient)
            .WithMany()
            .HasForeignKey("Patient_Id");


        // Diagnosis has one Encounter of type Encounter
        modelBuilder.Entity<Diagnosis>()
            .HasOne(x => x.Encounter)
            .WithMany()
            .HasForeignKey("Encounter_Id");

        // Diagnosis has one Patient of type Patient
        modelBuilder.Entity<Diagnosis>()
            .HasOne(x => x.Patient)
            .WithMany()
            .HasForeignKey("Patient_Id");


        // Observation has one Encounter of type Encounter
        modelBuilder.Entity<Observation>()
            .HasOne(x => x.Encounter)
            .WithMany()
            .HasForeignKey("Encounter_Id");

        // Observation has one Patient of type Patient
        modelBuilder.Entity<Observation>()
            .HasOne(x => x.Patient)
            .WithMany()
            .HasForeignKey("Patient_Id");

        // Observation has one Device of type MedicalDevice
        modelBuilder.Entity<Observation>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("Device_Id");

        // Observation has one LabResult of type LabResult
        modelBuilder.Entity<Observation>()
            .HasOne(x => x.LabResult)
            .WithMany()
            .HasForeignKey("LabResult_Id");


        // CarePlan has one Patient of type Patient
        modelBuilder.Entity<CarePlan>()
            .HasOne(x => x.Patient)
            .WithMany()
            .HasForeignKey("Patient_Id");

        // CarePlan has one CareTeam of type CareTeam
        modelBuilder.Entity<CarePlan>()
            .HasOne(x => x.CareTeam)
            .WithMany()
            .HasForeignKey("CareTeam_Id");


        // CarePlan has one or more Encounters of type Encounter
        modelBuilder.Entity<Encounter>()
            .HasOne<CarePlan>()
            .WithMany(parent => parent.Encounters)
            .HasForeignKey("CarePlan_Id");

        // CarePlan has one or more Tasks of type CareTask
        modelBuilder.Entity<CareTask>()
            .HasOne<CarePlan>()
            .WithMany(parent => parent.Tasks)
            .HasForeignKey("CarePlan_Id");

        // CareTask has one CarePlan of type CarePlan
        modelBuilder.Entity<CareTask>()
            .HasOne(x => x.CarePlan)
            .WithMany()
            .HasForeignKey("CarePlan_Id");

        // CareTask has one AssignedTo of type Clinician
        modelBuilder.Entity<CareTask>()
            .HasOne(x => x.AssignedTo)
            .WithMany()
            .HasForeignKey("AssignedTo_Id");

        // CareTask has one Encounter of type Encounter
        modelBuilder.Entity<CareTask>()
            .HasOne(x => x.Encounter)
            .WithMany()
            .HasForeignKey("Encounter_Id");


        // Allergy has one Patient of type Patient
        modelBuilder.Entity<Allergy>()
            .HasOne(x => x.Patient)
            .WithMany()
            .HasForeignKey("Patient_Id");


        // Condition has one Patient of type Patient
        modelBuilder.Entity<Condition>()
            .HasOne(x => x.Patient)
            .WithMany()
            .HasForeignKey("Patient_Id");



        // InsurancePayer has one or more Plans of type InsurancePlan
        modelBuilder.Entity<InsurancePlan>()
            .HasOne<InsurancePayer>()
            .WithMany(parent => parent.Plans)
            .HasForeignKey("InsurancePayer_Id");

        // InsurancePayer has one or more Claims of type Claim
        modelBuilder.Entity<Claim>()
            .HasOne<InsurancePayer>()
            .WithMany(parent => parent.Claims)
            .HasForeignKey("InsurancePayer_Id");

        // InsurancePlan has one Payer of type InsurancePayer
        modelBuilder.Entity<InsurancePlan>()
            .HasOne(x => x.Payer)
            .WithMany()
            .HasForeignKey("Payer_Id");


        // InsurancePlan has one or more Coverages of type Coverage
        modelBuilder.Entity<Coverage>()
            .HasOne<InsurancePlan>()
            .WithMany(parent => parent.Coverages)
            .HasForeignKey("InsurancePlan_Id");

        // Coverage has one Patient of type Patient
        modelBuilder.Entity<Coverage>()
            .HasOne(x => x.Patient)
            .WithMany()
            .HasForeignKey("Patient_Id");

        // Coverage has one Plan of type InsurancePlan
        modelBuilder.Entity<Coverage>()
            .HasOne(x => x.Plan)
            .WithMany()
            .HasForeignKey("Plan_Id");


        // Coverage has one or more Claims of type Claim
        modelBuilder.Entity<Claim>()
            .HasOne<Coverage>()
            .WithMany(parent => parent.Claims)
            .HasForeignKey("Coverage_Id");

        // Coverage has one or more Authorizations of type Authorization
        modelBuilder.Entity<Authorization>()
            .HasOne<Coverage>()
            .WithMany(parent => parent.Authorizations)
            .HasForeignKey("Coverage_Id");

        // Claim has one Patient of type Patient
        modelBuilder.Entity<Claim>()
            .HasOne(x => x.Patient)
            .WithMany()
            .HasForeignKey("Patient_Id");

        // Claim has one Coverage of type Coverage
        modelBuilder.Entity<Claim>()
            .HasOne(x => x.Coverage)
            .WithMany()
            .HasForeignKey("Coverage_Id");

        // Claim has one Encounter of type Encounter
        modelBuilder.Entity<Claim>()
            .HasOne(x => x.Encounter)
            .WithMany()
            .HasForeignKey("Encounter_Id");

        // Claim has one Payer of type InsurancePayer
        modelBuilder.Entity<Claim>()
            .HasOne(x => x.Payer)
            .WithMany()
            .HasForeignKey("Payer_Id");


        // Claim has one or more Invoices of type Invoice
        modelBuilder.Entity<Invoice>()
            .HasOne<Claim>()
            .WithMany(parent => parent.Invoices)
            .HasForeignKey("Claim_Id");

        // Authorization has one Coverage of type Coverage
        modelBuilder.Entity<Authorization>()
            .HasOne(x => x.Coverage)
            .WithMany()
            .HasForeignKey("Coverage_Id");

        // Authorization has one Order of type ClinicalOrder
        modelBuilder.Entity<Authorization>()
            .HasOne(x => x.Order)
            .WithMany()
            .HasForeignKey("Order_Id");


        // Invoice has one Patient of type Patient
        modelBuilder.Entity<Invoice>()
            .HasOne(x => x.Patient)
            .WithMany()
            .HasForeignKey("Patient_Id");

        // Invoice has one Claim of type Claim
        modelBuilder.Entity<Invoice>()
            .HasOne(x => x.Claim)
            .WithMany()
            .HasForeignKey("Claim_Id");


        // Invoice has one or more Payments of type Payment
        modelBuilder.Entity<Payment>()
            .HasOne<Invoice>()
            .WithMany(parent => parent.Payments)
            .HasForeignKey("Invoice_Id");

        // Payment has one Invoice of type Invoice
        modelBuilder.Entity<Payment>()
            .HasOne(x => x.Invoice)
            .WithMany()
            .HasForeignKey("Invoice_Id");

        // Payment has one Payer of type InsurancePayer
        modelBuilder.Entity<Payment>()
            .HasOne(x => x.Payer)
            .WithMany()
            .HasForeignKey("Payer_Id");


        // MedicalDevice has one Patient of type Patient
        modelBuilder.Entity<MedicalDevice>()
            .HasOne(x => x.Patient)
            .WithMany()
            .HasForeignKey("Patient_Id");


        // MedicalDevice has one or more Observations of type Observation
        modelBuilder.Entity<Observation>()
            .HasOne<MedicalDevice>()
            .WithMany(parent => parent.Observations)
            .HasForeignKey("MedicalDevice_Id");

        // MedicalDevice has one or more SoftwareUpdates of type SoftwareUpdate
        modelBuilder.Entity<SoftwareUpdate>()
            .HasOne<MedicalDevice>()
            .WithMany(parent => parent.SoftwareUpdates)
            .HasForeignKey("MedicalDevice_Id");

        // SoftwareUpdate has one Device of type MedicalDevice
        modelBuilder.Entity<SoftwareUpdate>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("Device_Id");



        // MedicalSupplier has one or more Facilities of type Facility
        modelBuilder.Entity<Facility>()
            .HasOne<MedicalSupplier>()
            .WithMany(parent => parent.Facilities)
            .HasForeignKey("MedicalSupplier_Id");

        // MedicalSupplier has one or more InventoryItems of type InventoryItem
        modelBuilder.Entity<InventoryItem>()
            .HasOne<MedicalSupplier>()
            .WithMany(parent => parent.InventoryItems)
            .HasForeignKey("MedicalSupplier_Id");

        // InventoryItem has one Facility of type Facility
        modelBuilder.Entity<InventoryItem>()
            .HasOne(x => x.Facility)
            .WithMany()
            .HasForeignKey("Facility_Id");

        // InventoryItem has one Supplier of type MedicalSupplier
        modelBuilder.Entity<InventoryItem>()
            .HasOne(x => x.Supplier)
            .WithMany()
            .HasForeignKey("Supplier_Id");


    }
}
