using static System.Math;

namespace DistanceCalculatorApi.Services;
public static class MathService
{
    const int EarthRadius = 6371;
    /// <summary>
    /// Используется формула Гаверсинуса
    /// </summary>
    /// <returns>Расстояние в километрах</returns>
    public static double GetDistanceBetween(Location from, Location to)
    {
        var latInRad = Deg2rad(to.Lat - from.Lat); 
        var lonInRad = Deg2rad(to.Lon - from.Lon);
        var intermediatVariable =           
            Sin(latInRad / 2) * Sin(latInRad / 2) +
            Cos(Deg2rad(from.Lat)) * Cos(Deg2rad(to.Lat)) *
            Sin(lonInRad / 2) * Sin(lonInRad / 2);

        var centralCorner = 2 * Atan2(Sqrt(intermediatVariable), Sqrt(1 - intermediatVariable));
        return EarthRadius * centralCorner;

    }
    /// <summary>
    /// Преобразует градусы в радианы
    /// </summary>
    /// <param name="deg"></param>
    /// <returns></returns>
    private static double Deg2rad(double deg)
    {
        return deg * (PI / 180);
    }
}