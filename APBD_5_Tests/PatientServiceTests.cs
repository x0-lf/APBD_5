using APBD_5.DTOs;
using APBD_5.Repositories;
using APBD_5.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace APBD_5;

public class PatientServiceTests
{
    private readonly Mock<IPatientRepository> _patientRepoMock = new();
    private readonly PatientService _patientService;

    public PatientServiceTests()
    {
        var logger = new LoggerFactory().CreateLogger<PatientService>();
        _patientService = new PatientService(_patientRepoMock.Object, logger);
    }

    [Fact]
    public async Task ShouldReturnNullWhenPatientNotFound()
    {
        _patientRepoMock.Setup(r => r.GetPatientWithPrescriptionsAsync(It.IsAny<int>()))
            .ReturnsAsync((PatientResponseDto?)null);

        var result = await _patientService.GetPatientAsync(1);
        Assert.Null(result);
    }

    [Fact]
    public async Task ShouldReturnPatientWhenExists()
    {
        var dto = new PatientResponseDto
        {
            IdPatient = 1,
            FirstName = "Test"
        };

        _patientRepoMock.Setup(r => r.GetPatientWithPrescriptionsAsync(1))
            .ReturnsAsync(dto);

        var result = await _patientService.GetPatientAsync(1);
        Assert.Equal(1, result?.IdPatient);
    }
}