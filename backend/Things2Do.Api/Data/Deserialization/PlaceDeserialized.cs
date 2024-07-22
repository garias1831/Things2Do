using System.Text.Json.Serialization;

namespace Things2Do.Api.Data.Deserialization;

//Note -- in the future this folder may contain things like entities, migrations etc
//May have to move / change namespace / class name for deserialization objz

public record class PlaceDeserialized
(
    [property: JsonPropertyName("title")]
    string Title,

    [property: JsonPropertyName("distance")]
    double Distance,

    [property: JsonPropertyName("position")]
    PlacePositionDeserialized Position

);