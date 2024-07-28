using System.Text.Json.Serialization;
using Things2Do.Api.Data.Deserialization.Contacts;
using Things2Do.Api.Data.Deserialization.OpeningHours;

namespace Things2Do.Api.Data.Deserialization;

//Note -- in the future this folder may contain things like entities, migrations etc
//May have to move / change namespace / class name for deserialization objz

public record class PlaceDeserialized
(
    [property: JsonPropertyName("title")]
    string Title,

    [property: JsonPropertyName("resultType")]
    string ResultType,

    [property: JsonPropertyName("distance")]
    double Distance, //TODO -- may or may not keep

    [property: JsonPropertyName("address")]
    PlaceAddressDeserialized Address,

    [property: JsonPropertyName("position")]
    PlacePositionDeserialized Position,

    [property: JsonPropertyName("contacts")]
    List<ContactItemDeserialized>? Contacts,

    [property: JsonPropertyName("openingHours")]
    List<OpeningHoursDeserialized>? OpeningHours

);