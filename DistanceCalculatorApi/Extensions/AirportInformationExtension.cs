using DistanceCalculatorApi.Services;

namespace DistanceCalculatorApi.Extensions;

public static class AirportInformationExtensions
{
    /// <summary>
    /// Рассчитать расстояние от выбранного аэропорта до другого
    /// </summary>
    /// <returns></returns>
    public static double DistanceTo(this AirportInformationModel from, Location to)
    {
        return MathService.GetDistanceBetween(from.Location, to);
    }
}
