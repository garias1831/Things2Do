using System.Text.Json.Serialization;

namespace Things2Do.Api.Data.Deserialization;

public record class PlaceAddressDeserialized
{
    private string _label;

    [property: JsonPropertyName("label")] //The full adress string
    public required string Label //NOTE -- may NOT want to make this required
    {
        get {return _label;}
        init
        {
            //Cut out the place name in the address result
            //+2 for the index of the comma plus an additonal space
            _label = value.Substring(value.IndexOf(',') + 2);
        }
    }
}