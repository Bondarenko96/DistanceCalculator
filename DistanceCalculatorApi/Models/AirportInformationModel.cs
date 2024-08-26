using System.Text.Json.Serialization;

public struct Location
{
    [JsonPropertyName("lon")]
    /// <summary>
    /// Широта
    /// </summary>
    public double Lon { get; set; }

    [JsonPropertyName("lat")]
    /// <summary>
    /// Долгота
    /// </summary>
    public double Lat { get; set; }
}

public class AirportInformationModel
{
    
    [JsonPropertyName("iata")]
    public string Iata { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("city_data")]
    public string CityIata { get; set; }

    [JsonPropertyName("icao")]
    public string Icao { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }
    
    [JsonPropertyName("country_iata")]
    public string CountryIata { get; set; }
    
    [JsonPropertyName("location")]
    public Location Location { get; set; }
    
    [JsonPropertyName("rating")]
    public int Rating { get; set; }

    [JsonPropertyName("hubs")]
    public int Hubs { get; set; }

    [JsonPropertyName("timezone_region_name")]
    public string TimezoneRegionName { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }
}

