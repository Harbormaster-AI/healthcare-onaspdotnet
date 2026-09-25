
using healthcareonaspdotnet.Contracts;
using healthcareonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace healthcareonaspdotnet.Persistence;

public class PatientRepository : IPatientRepository
{
    private readonly ApplicationDbContext _db;

    public PatientRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Patient?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Patients
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Patient>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Patients
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Patient patient, CancellationToken cancellationToken)
    {
        _db.Patients.Add(patient);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Patient patient, CancellationToken cancellationToken)
    {
        _db.Patients.Update(patient);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Patient patient, CancellationToken cancellationToken)
    {
        _db.Patients.Remove(patient);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToAppointmentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Appointments
            .Where(appointment =>
                request.ChildIds.Contains(appointment.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    appointment =>
                        EF.Property<Guid?>(
                            appointment,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromAppointmentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Appointments
            .Where(appointment =>
                request.ChildIds.Contains(appointment.Id) &&
                EF.Property<Guid?>(
                    appointment,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    appointment =>
                        EF.Property<Guid?>(
                            appointment,
                            "InventoryItem_Id"),
                    (Guid?)null));
    }


    public async Task AddToEncountersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Encounters
            .Where(encounter =>
                request.ChildIds.Contains(encounter.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    encounter =>
                        EF.Property<Guid?>(
                            encounter,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromEncountersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Encounters
            .Where(encounter =>
                request.ChildIds.Contains(encounter.Id) &&
                EF.Property<Guid?>(
                    encounter,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    encounter =>
                        EF.Property<Guid?>(
                            encounter,
                            "InventoryItem_Id"),
                    (Guid?)null));
    }


    public async Task AddToCarePlansAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CarePlans
            .Where(carePlan =>
                request.ChildIds.Contains(carePlan.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    carePlan =>
                        EF.Property<Guid?>(
                            carePlan,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromCarePlansAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CarePlans
            .Where(carePlan =>
                request.ChildIds.Contains(carePlan.Id) &&
                EF.Property<Guid?>(
                    carePlan,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    carePlan =>
                        EF.Property<Guid?>(
                            carePlan,
                            "InventoryItem_Id"),
                    (Guid?)null));
    }


    public async Task AddToAllergiesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Allergys
            .Where(allergy =>
                request.ChildIds.Contains(allergy.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    allergy =>
                        EF.Property<Guid?>(
                            allergy,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromAllergiesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Allergys
            .Where(allergy =>
                request.ChildIds.Contains(allergy.Id) &&
                EF.Property<Guid?>(
                    allergy,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    allergy =>
                        EF.Property<Guid?>(
                            allergy,
                            "InventoryItem_Id"),
                    (Guid?)null));
    }


    public async Task AddToConditionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Conditions
            .Where(condition =>
                request.ChildIds.Contains(condition.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    condition =>
                        EF.Property<Guid?>(
                            condition,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromConditionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Conditions
            .Where(condition =>
                request.ChildIds.Contains(condition.Id) &&
                EF.Property<Guid?>(
                    condition,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    condition =>
                        EF.Property<Guid?>(
                            condition,
                            "InventoryItem_Id"),
                    (Guid?)null));
    }


    public async Task AddToMedicationOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.MedicationOrders
            .Where(medicationOrder =>
                request.ChildIds.Contains(medicationOrder.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    medicationOrder =>
                        EF.Property<Guid?>(
                            medicationOrder,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromMedicationOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.MedicationOrders
            .Where(medicationOrder =>
                request.ChildIds.Contains(medicationOrder.Id) &&
                EF.Property<Guid?>(
                    medicationOrder,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    medicationOrder =>
                        EF.Property<Guid?>(
                            medicationOrder,
                            "InventoryItem_Id"),
                    (Guid?)null));
    }


    public async Task AddToLabOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.LaboratoryOrders
            .Where(laboratoryOrder =>
                request.ChildIds.Contains(laboratoryOrder.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    laboratoryOrder =>
                        EF.Property<Guid?>(
                            laboratoryOrder,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromLabOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.LaboratoryOrders
            .Where(laboratoryOrder =>
                request.ChildIds.Contains(laboratoryOrder.Id) &&
                EF.Property<Guid?>(
                    laboratoryOrder,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    laboratoryOrder =>
                        EF.Property<Guid?>(
                            laboratoryOrder,
                            "InventoryItem_Id"),
                    (Guid?)null));
    }


    public async Task AddToImagingOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ImagingOrders
            .Where(imagingOrder =>
                request.ChildIds.Contains(imagingOrder.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    imagingOrder =>
                        EF.Property<Guid?>(
                            imagingOrder,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromImagingOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ImagingOrders
            .Where(imagingOrder =>
                request.ChildIds.Contains(imagingOrder.Id) &&
                EF.Property<Guid?>(
                    imagingOrder,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    imagingOrder =>
                        EF.Property<Guid?>(
                            imagingOrder,
                            "InventoryItem_Id"),
                    (Guid?)null));
    }


    public async Task AddToCoveragesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Coverages
            .Where(coverage =>
                request.ChildIds.Contains(coverage.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    coverage =>
                        EF.Property<Guid?>(
                            coverage,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromCoveragesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Coverages
            .Where(coverage =>
                request.ChildIds.Contains(coverage.Id) &&
                EF.Property<Guid?>(
                    coverage,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    coverage =>
                        EF.Property<Guid?>(
                            coverage,
                            "InventoryItem_Id"),
                    (Guid?)null));
    }


    public async Task AddToClaimsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Claims
            .Where(claim =>
                request.ChildIds.Contains(claim.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    claim =>
                        EF.Property<Guid?>(
                            claim,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromClaimsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Claims
            .Where(claim =>
                request.ChildIds.Contains(claim.Id) &&
                EF.Property<Guid?>(
                    claim,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    claim =>
                        EF.Property<Guid?>(
                            claim,
                            "InventoryItem_Id"),
                    (Guid?)null));
    }


    public async Task AddToDevicesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.MedicalDevices
            .Where(medicalDevice =>
                request.ChildIds.Contains(medicalDevice.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    medicalDevice =>
                        EF.Property<Guid?>(
                            medicalDevice,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromDevicesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.MedicalDevices
            .Where(medicalDevice =>
                request.ChildIds.Contains(medicalDevice.Id) &&
                EF.Property<Guid?>(
                    medicalDevice,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    medicalDevice =>
                        EF.Property<Guid?>(
                            medicalDevice,
                            "InventoryItem_Id"),
                    (Guid?)null));
    }


    public async Task AddToObservationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Observations
            .Where(observation =>
                request.ChildIds.Contains(observation.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    observation =>
                        EF.Property<Guid?>(
                            observation,
                            "InventoryItem_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromObservationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Observations
            .Where(observation =>
                request.ChildIds.Contains(observation.Id) &&
                EF.Property<Guid?>(
                    observation,
                    "InventoryItem_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    observation =>
                        EF.Property<Guid?>(
                            observation,
                            "InventoryItem_Id"),
                    (Guid?)null));
    }

}
