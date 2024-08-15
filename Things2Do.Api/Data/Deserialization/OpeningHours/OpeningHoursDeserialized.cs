using System.Text.Json.Serialization;

namespace Things2Do.Api.Data.Deserialization.OpeningHours;

public record class OpeningHoursDeserialized
(
    [property: JsonPropertyName("isOpen")]
    bool IsOpen,

    //TODO -- if hours are too hard to parse might add the ICalendar structred
    [property: JsonPropertyName("structured")]
    List<TimeEntryDeserialized> Values
);