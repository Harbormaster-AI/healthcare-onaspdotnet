using healthcareonaspdotnet.Domain;

namespace healthcareonaspdotnet.Contracts;

public class IdentifierRequest
{
    public Guid Id { get; set; }
}

public class AssociationRequest
{
    public Guid ParentId { get; set; }
    public Guid ChildId { get; set; }
}

public class MultipleAssociationRequest
{
    public Guid ParentId { get; set; }
    public List<Guid> ChildIds { get; set; } = new();
}

public class HealthSystemRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? LegalName { get; set; } 
 public virtual string? HeadquartersCountry { get; set; } 
 public virtual string? Website { get; set; } 
}

public class HealthSystemResponse : HealthSystemRequest {
    public static HealthSystemResponse FromModel(HealthSystem model) {
        return new HealthSystemResponse {
            Id = model.Id,
            Name = model.Name,
            LegalName = model.LegalName,
            HeadquartersCountry = model.HeadquartersCountry,
            Website = model.Website,
        };
    }
}

public class FacilityRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? FacilityCode { get; set; } 
 public virtual Address? Address { get; set; } 
 public virtual FacilityType? FacilityType { get; set; } 
}

public class FacilityResponse : FacilityRequest {
    public static FacilityResponse FromModel(Facility model) {
        return new FacilityResponse {
            Id = model.Id,
            Name = model.Name,
            FacilityCode = model.FacilityCode,
            Address = model.Address,
            FacilityType = model.FacilityType,
        };
    }
}

public class DepartmentRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual DepartmentType? DepartmentType { get; set; } 
}

public class DepartmentResponse : DepartmentRequest {
    public static DepartmentResponse FromModel(Department model) {
        return new DepartmentResponse {
            Id = model.Id,
            Name = model.Name,
            DepartmentType = model.DepartmentType,
        };
    }
}

public class CareTeamRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual CareSettingType? CareSetting { get; set; } 
}

public class CareTeamResponse : CareTeamRequest {
    public static CareTeamResponse FromModel(CareTeam model) {
        return new CareTeamResponse {
            Id = model.Id,
            Name = model.Name,
            CareSetting = model.CareSetting,
        };
    }
}

public class ClinicianRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? FirstName { get; set; } 
 public virtual string? LastName { get; set; } 
 public virtual string? LicenseNumber { get; set; } 
 public virtual ClinicianType? ClinicianType { get; set; } 
 public virtual ClinicianSpecialty? Specialty { get; set; } 
}

public class ClinicianResponse : ClinicianRequest {
    public static ClinicianResponse FromModel(Clinician model) {
        return new ClinicianResponse {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            LicenseNumber = model.LicenseNumber,
            ClinicianType = model.ClinicianType,
            Specialty = model.Specialty,
        };
    }
}

public class PatientRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? FirstName { get; set; } 
 public virtual string? LastName { get; set; } 
 public virtual MRN? Mrn { get; set; } 
 public virtual DateOnly? DateOfBirth { get; set; } 
 public virtual Address? Address { get; set; } 
 public virtual string? PrimaryLanguage { get; set; } 
 public virtual AdministrativeSex? SexAtBirth { get; set; } 
 public virtual BloodType? BloodType { get; set; } 
}

public class PatientResponse : PatientRequest {
    public static PatientResponse FromModel(Patient model) {
        return new PatientResponse {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Mrn = model.Mrn,
            DateOfBirth = model.DateOfBirth,
            Address = model.Address,
            PrimaryLanguage = model.PrimaryLanguage,
            SexAtBirth = model.SexAtBirth,
            BloodType = model.BloodType,
        };
    }
}

public class AppointmentRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual DateTime? AppointmentDate { get; set; } 
 public virtual string? Reason { get; set; } 
 public virtual AppointmentStatus? Status { get; set; } 
 public virtual Priority? Priority { get; set; } 
}

public class AppointmentResponse : AppointmentRequest {
    public static AppointmentResponse FromModel(Appointment model) {
        return new AppointmentResponse {
            Id = model.Id,
            AppointmentDate = model.AppointmentDate,
            Reason = model.Reason,
            Status = model.Status,
            Priority = model.Priority,
        };
    }
}

public class EncounterRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? EncounterNumber { get; set; } 
 public virtual DateTime? StartDateTime { get; set; } 
 public virtual DateTime? EndDateTime { get; set; } 
 public virtual EncounterStatus? Status { get; set; } 
 public virtual EncounterType? EncounterType { get; set; } 
}

public class EncounterResponse : EncounterRequest {
    public static EncounterResponse FromModel(Encounter model) {
        return new EncounterResponse {
            Id = model.Id,
            EncounterNumber = model.EncounterNumber,
            StartDateTime = model.StartDateTime,
            EndDateTime = model.EndDateTime,
            Status = model.Status,
            EncounterType = model.EncounterType,
        };
    }
}

public class AdmissionRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual DateTime? AdmitDateTime { get; set; } 
 public virtual string? Bed { get; set; } 
 public virtual AdmissionType? AdmissionType { get; set; } 
}

public class AdmissionResponse : AdmissionRequest {
    public static AdmissionResponse FromModel(Admission model) {
        return new AdmissionResponse {
            Id = model.Id,
            AdmitDateTime = model.AdmitDateTime,
            Bed = model.Bed,
            AdmissionType = model.AdmissionType,
        };
    }
}

public class DischargeRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual DateTime? DischargeDateTime { get; set; } 
 public virtual DischargeDisposition? Disposition { get; set; } 
}

public class DischargeResponse : DischargeRequest {
    public static DischargeResponse FromModel(Discharge model) {
        return new DischargeResponse {
            Id = model.Id,
            DischargeDateTime = model.DischargeDateTime,
            Disposition = model.Disposition,
        };
    }
}

public class ClinicalOrderRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? OrderNumber { get; set; } 
 public virtual OrderStatus? Status { get; set; } 
 public virtual ClinicalOrderType? OrderType { get; set; } 
 public virtual Priority? Priority { get; set; } 
}

public class ClinicalOrderResponse : ClinicalOrderRequest {
    public static ClinicalOrderResponse FromModel(ClinicalOrder model) {
        return new ClinicalOrderResponse {
            Id = model.Id,
            OrderNumber = model.OrderNumber,
            Status = model.Status,
            OrderType = model.OrderType,
            Priority = model.Priority,
        };
    }
}

public class MedicationOrderRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? MedicationCode { get; set; } 
 public virtual Dose? Dose { get; set; } 
 public virtual string? Frequency { get; set; } 
 public virtual string? Duration { get; set; } 
 public virtual RouteOfAdministration? Route { get; set; } 
}

public class MedicationOrderResponse : MedicationOrderRequest {
    public static MedicationOrderResponse FromModel(MedicationOrder model) {
        return new MedicationOrderResponse {
            Id = model.Id,
            MedicationCode = model.MedicationCode,
            Dose = model.Dose,
            Frequency = model.Frequency,
            Duration = model.Duration,
            Route = model.Route,
        };
    }
}

public class LaboratoryRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? CliaNumber { get; set; } 
}

public class LaboratoryResponse : LaboratoryRequest {
    public static LaboratoryResponse FromModel(Laboratory model) {
        return new LaboratoryResponse {
            Id = model.Id,
            Name = model.Name,
            CliaNumber = model.CliaNumber,
        };
    }
}

public class LaboratoryOrderRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? TestCode { get; set; } 
 public virtual bool? FastingRequired { get; set; } 
 public virtual SpecimenType? SpecimenType { get; set; } 
}

public class LaboratoryOrderResponse : LaboratoryOrderRequest {
    public static LaboratoryOrderResponse FromModel(LaboratoryOrder model) {
        return new LaboratoryOrderResponse {
            Id = model.Id,
            TestCode = model.TestCode,
            FastingRequired = model.FastingRequired,
            SpecimenType = model.SpecimenType,
        };
    }
}

public class LabResultRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? ResultCode { get; set; } 
 public virtual DateTime? IssuedDate { get; set; } 
 public virtual ResultStatus? Status { get; set; } 
}

public class LabResultResponse : LabResultRequest {
    public static LabResultResponse FromModel(LabResult model) {
        return new LabResultResponse {
            Id = model.Id,
            ResultCode = model.ResultCode,
            IssuedDate = model.IssuedDate,
            Status = model.Status,
        };
    }
}

public class ImagingCenterRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
}

public class ImagingCenterResponse : ImagingCenterRequest {
    public static ImagingCenterResponse FromModel(ImagingCenter model) {
        return new ImagingCenterResponse {
            Id = model.Id,
            Name = model.Name,
        };
    }
}

public class ImagingOrderRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? BodySite { get; set; } 
 public virtual bool? Contrast { get; set; } 
 public virtual ImagingModality? Modality { get; set; } 
}

public class ImagingOrderResponse : ImagingOrderRequest {
    public static ImagingOrderResponse FromModel(ImagingOrder model) {
        return new ImagingOrderResponse {
            Id = model.Id,
            BodySite = model.BodySite,
            Contrast = model.Contrast,
            Modality = model.Modality,
        };
    }
}

public class ImagingReportRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? ReportNumber { get; set; } 
 public virtual string? Impression { get; set; } 
 public virtual DateTime? ReportedDate { get; set; } 
 public virtual ResultStatus? Status { get; set; } 
}

public class ImagingReportResponse : ImagingReportRequest {
    public static ImagingReportResponse FromModel(ImagingReport model) {
        return new ImagingReportResponse {
            Id = model.Id,
            ReportNumber = model.ReportNumber,
            Impression = model.Impression,
            ReportedDate = model.ReportedDate,
            Status = model.Status,
        };
    }
}

public class ProcedureOrderRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? ProcedureCode { get; set; } 
 public virtual bool? ConsentObtained { get; set; } 
 public virtual AnesthesiaType? AnesthesiaType { get; set; } 
}

public class ProcedureOrderResponse : ProcedureOrderRequest {
    public static ProcedureOrderResponse FromModel(ProcedureOrder model) {
        return new ProcedureOrderResponse {
            Id = model.Id,
            ProcedureCode = model.ProcedureCode,
            ConsentObtained = model.ConsentObtained,
            AnesthesiaType = model.AnesthesiaType,
        };
    }
}

public class ProcedureRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? ProcedureCode { get; set; } 
 public virtual DateTime? StartDateTime { get; set; } 
 public virtual DateTime? EndDateTime { get; set; } 
 public virtual ProcedureStatus? Status { get; set; } 
}

public class ProcedureResponse : ProcedureRequest {
    public static ProcedureResponse FromModel(Procedure model) {
        return new ProcedureResponse {
            Id = model.Id,
            ProcedureCode = model.ProcedureCode,
            StartDateTime = model.StartDateTime,
            EndDateTime = model.EndDateTime,
            Status = model.Status,
        };
    }
}

public class PharmacyRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
}

public class PharmacyResponse : PharmacyRequest {
    public static PharmacyResponse FromModel(Pharmacy model) {
        return new PharmacyResponse {
            Id = model.Id,
            Name = model.Name,
        };
    }
}

public class MedicationDispenseRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? DispenseNumber { get; set; } 
 public virtual decimal? Quantity { get; set; } 
 public virtual DateTime? WhenPrepared { get; set; } 
 public virtual DispenseStatus? Status { get; set; } 
}

public class MedicationDispenseResponse : MedicationDispenseRequest {
    public static MedicationDispenseResponse FromModel(MedicationDispense model) {
        return new MedicationDispenseResponse {
            Id = model.Id,
            DispenseNumber = model.DispenseNumber,
            Quantity = model.Quantity,
            WhenPrepared = model.WhenPrepared,
            Status = model.Status,
        };
    }
}

public class DiagnosisRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Code { get; set; } 
 public virtual string? Description { get; set; } 
 public virtual DateOnly? OnsetDate { get; set; } 
 public virtual DiagnosisCertainty? Certainty { get; set; } 
}

public class DiagnosisResponse : DiagnosisRequest {
    public static DiagnosisResponse FromModel(Diagnosis model) {
        return new DiagnosisResponse {
            Id = model.Id,
            Code = model.Code,
            Description = model.Description,
            OnsetDate = model.OnsetDate,
            Certainty = model.Certainty,
        };
    }
}

public class ObservationRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Code { get; set; } 
 public virtual string? Value { get; set; } 
 public virtual string? Unit { get; set; } 
 public virtual DateTime? EffectiveDateTime { get; set; } 
 public virtual ObservationInterpretation? Interpretation { get; set; } 
}

public class ObservationResponse : ObservationRequest {
    public static ObservationResponse FromModel(Observation model) {
        return new ObservationResponse {
            Id = model.Id,
            Code = model.Code,
            Value = model.Value,
            Unit = model.Unit,
            EffectiveDateTime = model.EffectiveDateTime,
            Interpretation = model.Interpretation,
        };
    }
}

public class CarePlanRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? PlanNumber { get; set; } 
 public virtual string? GoalSummary { get; set; } 
 public virtual CarePlanStatus? Status { get; set; } 
}

public class CarePlanResponse : CarePlanRequest {
    public static CarePlanResponse FromModel(CarePlan model) {
        return new CarePlanResponse {
            Id = model.Id,
            PlanNumber = model.PlanNumber,
            GoalSummary = model.GoalSummary,
            Status = model.Status,
        };
    }
}

public class CareTaskRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Description { get; set; } 
 public virtual DateOnly? DueDate { get; set; } 
 public virtual TaskStatus_? Status { get; set; } 
 public virtual Priority? Priority { get; set; } 
}

public class CareTaskResponse : CareTaskRequest {
    public static CareTaskResponse FromModel(CareTask model) {
        return new CareTaskResponse {
            Id = model.Id,
            Description = model.Description,
            DueDate = model.DueDate,
            Status = model.Status,
            Priority = model.Priority,
        };
    }
}

public class AllergyRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Substance { get; set; } 
 public virtual string? Reaction { get; set; } 
 public virtual AllergySeverity? Severity { get; set; } 
 public virtual AllergyStatus? Status { get; set; } 
}

public class AllergyResponse : AllergyRequest {
    public static AllergyResponse FromModel(Allergy model) {
        return new AllergyResponse {
            Id = model.Id,
            Substance = model.Substance,
            Reaction = model.Reaction,
            Severity = model.Severity,
            Status = model.Status,
        };
    }
}

public class ConditionRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Code { get; set; } 
 public virtual DateOnly? OnsetDate { get; set; } 
 public virtual DateOnly? AbatementDate { get; set; } 
 public virtual ConditionStatus? ClinicalStatus { get; set; } 
 public virtual DiagnosisCertainty? VerificationStatus { get; set; } 
}

public class ConditionResponse : ConditionRequest {
    public static ConditionResponse FromModel(Condition model) {
        return new ConditionResponse {
            Id = model.Id,
            Code = model.Code,
            OnsetDate = model.OnsetDate,
            AbatementDate = model.AbatementDate,
            ClinicalStatus = model.ClinicalStatus,
            VerificationStatus = model.VerificationStatus,
        };
    }
}

public class InsurancePayerRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Website { get; set; } 
 public virtual PayerType? PayerType { get; set; } 
}

public class InsurancePayerResponse : InsurancePayerRequest {
    public static InsurancePayerResponse FromModel(InsurancePayer model) {
        return new InsurancePayerResponse {
            Id = model.Id,
            Name = model.Name,
            Website = model.Website,
            PayerType = model.PayerType,
        };
    }
}

public class InsurancePlanRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? PlanCode { get; set; } 
 public virtual InsurancePlanType? PlanType { get; set; } 
}

public class InsurancePlanResponse : InsurancePlanRequest {
    public static InsurancePlanResponse FromModel(InsurancePlan model) {
        return new InsurancePlanResponse {
            Id = model.Id,
            Name = model.Name,
            PlanCode = model.PlanCode,
            PlanType = model.PlanType,
        };
    }
}

public class CoverageRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? MemberId { get; set; } 
 public virtual string? GroupNumber { get; set; } 
 public virtual DateOnly? EffectiveDate { get; set; } 
 public virtual DateOnly? EndDate { get; set; } 
 public virtual CoverageType? CoverageType { get; set; } 
}

public class CoverageResponse : CoverageRequest {
    public static CoverageResponse FromModel(Coverage model) {
        return new CoverageResponse {
            Id = model.Id,
            MemberId = model.MemberId,
            GroupNumber = model.GroupNumber,
            EffectiveDate = model.EffectiveDate,
            EndDate = model.EndDate,
            CoverageType = model.CoverageType,
        };
    }
}

public class ClaimRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? ClaimNumber { get; set; } 
 public virtual Money? TotalAmount { get; set; } 
 public virtual ClaimStatus? Status { get; set; } 
}

public class ClaimResponse : ClaimRequest {
    public static ClaimResponse FromModel(Claim model) {
        return new ClaimResponse {
            Id = model.Id,
            ClaimNumber = model.ClaimNumber,
            TotalAmount = model.TotalAmount,
            Status = model.Status,
        };
    }
}

public class AuthorizationRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? AuthNumber { get; set; } 
 public virtual string? RequestedService { get; set; } 
 public virtual AuthorizationStatus? Status { get; set; } 
}

public class AuthorizationResponse : AuthorizationRequest {
    public static AuthorizationResponse FromModel(Authorization model) {
        return new AuthorizationResponse {
            Id = model.Id,
            AuthNumber = model.AuthNumber,
            RequestedService = model.RequestedService,
            Status = model.Status,
        };
    }
}

public class InvoiceRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? InvoiceNumber { get; set; } 
 public virtual Money? TotalAmount { get; set; } 
 public virtual DateOnly? DueDate { get; set; } 
 public virtual InvoiceStatus? Status { get; set; } 
}

public class InvoiceResponse : InvoiceRequest {
    public static InvoiceResponse FromModel(Invoice model) {
        return new InvoiceResponse {
            Id = model.Id,
            InvoiceNumber = model.InvoiceNumber,
            TotalAmount = model.TotalAmount,
            DueDate = model.DueDate,
            Status = model.Status,
        };
    }
}

public class PaymentRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? PaymentNumber { get; set; } 
 public virtual Money? Amount { get; set; } 
 public virtual DateOnly? PaymentDate { get; set; } 
 public virtual PaymentMethod? Method { get; set; } 
}

public class PaymentResponse : PaymentRequest {
    public static PaymentResponse FromModel(Payment model) {
        return new PaymentResponse {
            Id = model.Id,
            PaymentNumber = model.PaymentNumber,
            Amount = model.Amount,
            PaymentDate = model.PaymentDate,
            Method = model.Method,
        };
    }
}

public class MedicalDeviceRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Udi { get; set; } 
 public virtual string? Manufacturer { get; set; } 
 public virtual DeviceType? DeviceType { get; set; } 
 public virtual DeviceConnectivityStatus? ConnectivityStatus { get; set; } 
}

public class MedicalDeviceResponse : MedicalDeviceRequest {
    public static MedicalDeviceResponse FromModel(MedicalDevice model) {
        return new MedicalDeviceResponse {
            Id = model.Id,
            Udi = model.Udi,
            Manufacturer = model.Manufacturer,
            DeviceType = model.DeviceType,
            ConnectivityStatus = model.ConnectivityStatus,
        };
    }
}

public class SoftwareUpdateRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Version { get; set; } 
 public virtual DateTime? AppliedDate { get; set; } 
 public virtual SoftwareUpdateType? UpdateType { get; set; } 
}

public class SoftwareUpdateResponse : SoftwareUpdateRequest {
    public static SoftwareUpdateResponse FromModel(SoftwareUpdate model) {
        return new SoftwareUpdateResponse {
            Id = model.Id,
            Version = model.Version,
            AppliedDate = model.AppliedDate,
            UpdateType = model.UpdateType,
        };
    }
}

public class MedicalSupplierRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Website { get; set; } 
 public virtual SupplierTier? SupplierTier { get; set; } 
}

public class MedicalSupplierResponse : MedicalSupplierRequest {
    public static MedicalSupplierResponse FromModel(MedicalSupplier model) {
        return new MedicalSupplierResponse {
            Id = model.Id,
            Name = model.Name,
            Website = model.Website,
            SupplierTier = model.SupplierTier,
        };
    }
}

public class InventoryItemRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Sku { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual int? QuantityOnHand { get; set; } 
 public virtual int? QuantityReserved { get; set; } 
}

public class InventoryItemResponse : InventoryItemRequest {
    public static InventoryItemResponse FromModel(InventoryItem model) {
        return new InventoryItemResponse {
            Id = model.Id,
            Sku = model.Sku,
            Name = model.Name,
            QuantityOnHand = model.QuantityOnHand,
            QuantityReserved = model.QuantityReserved,
        };
    }
}

