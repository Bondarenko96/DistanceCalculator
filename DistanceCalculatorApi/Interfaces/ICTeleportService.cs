namespace DistanceCalculatorApi.Interfaces;

public interface ICTeleportService
{
    public Task<AirportInformationModel> GetAirportInformation(string IATACode);
}