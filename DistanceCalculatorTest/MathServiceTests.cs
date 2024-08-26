using DistanceCalculatorApi.Services;

namespace DistanceCalculatorTest;

public class Tests
{
    [Test]
    public void GetDistanceBetween_SameLocation_ReturnsZero()
    {
        // Arrange
        var location = new Location { Lat = 52.5200, Lon = 13.4050 }; 

        // Act
        var distance = MathService.GetDistanceBetween(location, location);

        // Assert
        Assert.That(distance, Is.EqualTo(0));
    }

    [Test]
    public void GetDistanceBetween_KnownLocations_ReturnsCorrectDistance()
    {
        // Arrange
        var berlin = new Location { Lat = 52.5200, Lon = 13.4050 }; 
        var paris = new Location { Lat = 48.8566, Lon = 2.3522 }; 

        // Act
        var distance = MathService.GetDistanceBetween(berlin, paris);

        // Assert
        // Ожидаемое расстояние между Берлином и Парижем около 878 км
        Assert.That(Math.Round(distance, 2), Is.EqualTo(877.46));
    }

    [Test]
    public void GetDistanceBetween_PolarOpposites_ReturnsCorrectDistance()
    {
        // Arrange
        var northPole = new Location { Lat = 90.0, Lon = 0.0 }; 
        var southPole = new Location { Lat = -90.0, Lon = 0.0 };  

        // Act
        var distance = MathService.GetDistanceBetween(northPole, southPole);

        // Assert
        // Ожидаемое расстояние между полюсами — половина длины окружности Земли (около 20015 км)
        Assert.That(Math.Round(distance, 0), Is.EqualTo(20015));
    }
}