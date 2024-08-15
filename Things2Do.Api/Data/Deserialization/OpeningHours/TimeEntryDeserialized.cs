using System.Text.Json.Serialization;

namespace Things2Do.Api.Data.Deserialization;

//Hours of operation for a day in icalendar format
public record class TimeEntryDeserialized
(
    [property: JsonPropertyName("start")]
    string Start,

    [property: JsonPropertyName("duration")]
    string Duration,

    [property: JsonPropertyName("recurrence")]
    string Recurrence
);