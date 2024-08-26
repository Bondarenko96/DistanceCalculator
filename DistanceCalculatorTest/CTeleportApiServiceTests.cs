using System.Net;
using System.Text.Json;
using DistanceCalculatorApi.Interfaces;
using DistanceCalculatorApi.Services;
using Moq;
using Moq.Protected;


[TestFixture]
public class CTeleportApiServiceTests
{
    private Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private ICTeleportService _service;

    public CTeleportApiServiceTests()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        var client = new HttpClient(_httpMessageHandlerMock.Object);
        _service = new CTeleportApiService(client);
    }

    [Test]
    public async Task GetAirportInformation_ValidIATACode_ReturnsAirportInformation()
    {
        // Arrange
        var iataCode = "AMS";
        var expectedResponse = new AirportInformationModel
        {
            Name = "Amsterdam Airport Schiphol",
            City = "Amsterdam",
            Country = "Netherlands",
            Location = new Location
            {
                Lat = 52.3086,
                Lon = 4.7639
            },
        };

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(expectedResponse))
            });

        // Act
        var result = await _service.GetAirportInformation(iataCode);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Name, Is.EqualTo(expectedResponse.Name));
            Assert.That(result.City, Is.EqualTo(expectedResponse.City));
            Assert.That(result.Location, Is.EqualTo(expectedResponse.Location));
        });
    }

    [Test]
    public void GetAirportInformation_InvalidIATACode_ThrowsException()
    {
        // Arrange
        var iataCode = "INVALID";

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound
            });

        // Act & Assert
        var ex = Assert.ThrowsAsync<HttpRequestException>(() => _service.GetAirportInformation(iataCode));
        Assert.That(ex.Message, Is.Not.Null.And.Contains("404"));
    }
}