using System.Text.Json.Serialization;

namespace Things2Do.Api.Data.Deserialization.OpeningHours;

public record class OpeningHoursDeserialized
(
    [property: JsonPropertyName("text")]    
    List<string> Text,
    
    [property: JsonPropertyName("isOpen")]
    bool? IsOpen

    //TODO -- if hours are too hard to parse might add the ICalendar structred
);