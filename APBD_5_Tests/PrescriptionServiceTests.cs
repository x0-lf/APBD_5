using APBD_5.DTOs;
using APBD_5.Repositories;
using APBD_5.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace APBD_5;

public class PrescriptionServiceTests
{
    private readonly Mock<IPrescriptionRepository> _prescriptionRepoMock = new();
    private readonly PrescriptionService _prescriptionService;

    public PrescriptionServiceTests()
    {
        var logger = new LoggerFactory().CreateLogger<PrescriptionService>();
        _prescriptionService = new PrescriptionService(_prescriptionRepoMock.Object, logger);
    }

    [Fact]
    public async Task ShouldThrowWhenMoreThan10Medicaments()
    {
        var dto = new PrescriptionCreateRequestDto
        {
            Patient = new PatientDto
            {
                FirstName = "Test",
                LastName = "Test",
                BirthDate = new DateOnly(1999, 11, 1)
            },
            IdDoctor = 1,
            Date = new DateOnly(2025, 1, 1),
            DueDate = new DateOnly(2025, 5, 5),
            Medicaments = Enumerable.Range(1, 11).Select(i => new PrescriptionMedicamentDto
            {
                IdMedicament = i,
                Dose = 1,
                Details = "Test"
            }).ToList()
        };

        await Assert.ThrowsAsync<ArgumentException>(() => _prescriptionService.CreatePrescriptionAsync(dto));
    }

    [Fact]
    public async Task ShouldThrowWhenDueDateBeforeDate()
    {
        var dto = new PrescriptionCreateRequestDto
        {
            Patient = new PatientDto
            {
                FirstName = "Test",
                LastName = "Test",
                BirthDate = new DateOnly(1999, 11, 8)
            },
            IdDoctor = 1,
            Date = new DateOnly(2025, 9, 10),
            DueDate = new DateOnly(2025, 8, 1),
            Medicaments = new List<PrescriptionMedicamentDto>()
        };

        await Assert.ThrowsAsync<ArgumentException>(() => _prescriptionService.CreatePrescriptionAsync(dto));
    }

    [Fact]
    public async Task ShouldCallRepositoryOnValidRequest()
    {
        var dto = new PrescriptionCreateRequestDto
        {
            Patient = new PatientDto
            {
                FirstName = "Valid",
                LastName = "Patient",
                BirthDate = new DateOnly(2000, 6, 1)
            },
            IdDoctor = 1,
            Date = new DateOnly(2025, 1, 1),
            DueDate = new DateOnly(2025, 1, 11),
            Medicaments = Enumerable.Range(1, 2).Select(i => new PrescriptionMedicamentDto
            {
                IdMedicament = i,
                Dose = 1,
                Details = "Test"
            }).ToList()
        };

        _prescriptionRepoMock.Setup(r => r.CreatePrescriptionAsync(It.IsAny<PrescriptionCreateRequestDto>()))
            .ReturnsAsync(123);

        var id = await _prescriptionService.CreatePrescriptionAsync(dto);
        Assert.Equal(123, id);
    }
}