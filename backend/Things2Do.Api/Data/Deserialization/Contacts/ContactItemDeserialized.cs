
using System.Text.Json.Serialization;

namespace Things2Do.Api.Data.Deserialization.Contacts;

public record class ContactItemDeserialized
(
    [property: JsonPropertyName("phone")]
    List<PhoneContactDeserialized>? Phone,

    [property: JsonPropertyName("www")]
    List<WebContactDeserialized>? Web,

    [property: JsonPropertyName("email")]
    List<EmailContactDeserialized>? Email

);