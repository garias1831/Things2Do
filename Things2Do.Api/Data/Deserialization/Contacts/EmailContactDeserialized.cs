using System.Text.Json.Serialization;

namespace Things2Do.Api.Data.Deserialization.Contacts;

public record class EmailContactDeserialized
(
    [property: JsonPropertyName("label")]
    string? Label, 

    [property: JsonPropertyName("value")]
    string Value
);