using System.Text.Json;
using DistanceCalculatorApi.Interfaces;

namespace DistanceCalculatorApi.Services;
public class CTeleportApiService(HttpClient httpClient) : ICTeleportService
{
    private static readonly string Host = "https://places-dev.cteleport.com";
    
    public async Task<AirportInformationModel> GetAirportInformation(string IATACode)
    {
        try
        {
            var url = Host + $"/airports/{IATACode}";
            var response = await httpClient.GetAsync(url);

            if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new ArgumentException($"Аэропорт {IATACode} не найден");
            }
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();

            var airportInformationModel = JsonSerializer.Deserialize<AirportInformationModel>(responseData);

            if (airportInformationModel != null)
                return airportInformationModel;

            throw new InvalidOperationException("Не удалось распарсить данные");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Ошибка запроса: {e.Message}");
            throw;
        }
    }
}