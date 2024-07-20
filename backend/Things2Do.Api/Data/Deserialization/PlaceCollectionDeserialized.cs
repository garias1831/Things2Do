using System.Text.Json;
using System.Text.Json.Serialization;

namespace Things2Do.Api.Data.Deserialization;

//Not sure if theres a way to deserialize directly into collection of place objs
//But this works
public record class PlaceCollectionDeserialized
(
    [property: JsonPropertyName("items")]
    ICollection<PlaceDeserialized> Items //Could b a list tbh
);