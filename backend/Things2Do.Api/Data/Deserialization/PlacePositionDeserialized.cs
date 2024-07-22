using System.Text.Json.Serialization;

namespace Things2Do.Api.Data.Deserialization;
public record class PlacePositionDeserialized
(
    [property: JsonPropertyName("lat")]
    decimal Lat,
    
    [property: JsonPropertyName("lng")]    
    decimal Lng
);