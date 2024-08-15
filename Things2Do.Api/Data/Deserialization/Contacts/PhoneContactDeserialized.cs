using System.Text.Json.Serialization;

namespace Things2Do.Api.Data.Deserialization.Contacts;

public record class PhoneContactDeserialized
(
    //NOTE -- the same as the other 2 contact data things, but prob fine
    //Will make interfact if neede
    [property: JsonPropertyName("label")]
    string? Label, 

    [property: JsonPropertyName("value")]
    string Value
);