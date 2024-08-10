using System.Net.Http.Headers;
using Things2Do.Api.Data.Deserialization;

namespace Things2Do.Api.Services;

//NOTE -- may want to store the api key here (access from somewhere private)
public class HereService
{
    private readonly HttpClient _httpClient;

    //This may b bad practice but not sure
    private readonly string _apiKey;

    //TODO -- may want to have a seperate class OR option to use the browse vs discover endpoints
    //Browse has filtering but ranks based on distance whereas discover 
    //ranks based on relevance to a text query
    public HereService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://browse.search.hereapi.com/v1/browse");
        //NOTE -- not sure if i need this
        //To be able to recieve json responses
        _httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json")
        );
        
        _apiKey = Environment.GetEnvironmentVariable("HereKey")!;
    }

    public async Task<List<PlaceDeserialized>> GetPlacesAsync(decimal lat, decimal lng)
    {
        //NOTE -- the lang=en param here returns place names, address, etc in ENGLISH
        //For future may want to find lang of page and shove it in here + render days of week in local language / culture
        //Or render in client lang w/ subtitle of native lang
        
        //NOTE - SAMPLE CATEGORY GROUPING
        //Food and drink: 100-1000-0000 (generic),300-3000-0065, 300-3000-0351, 300-3000-0351  (brevwey distillery winery)  
        //Enteretainment just the norm
        //Museums: 300-3100-0000, 300-3000-0024 (generic, artgallery)
        //Landmarks and monuments: all cats except winery and breqery

        var placeCollection = await _httpClient.GetFromJsonAsync<PlaceCollectionDeserialized>(
            $"?at={lat},{lng}&lang=en&apiKey={_apiKey}"
        );
        
        if (placeCollection is null)
        {
            return new List<PlaceDeserialized>();
        }

        // foreach (PlaceDeserialized place in placeCollection.Items)
        // {
        //     System.Console.WriteLine($"{place.Title} at: {place.Position.Lat}, {place.Position.Lng}");
        //     // System.Console.WriteLine(place.Distance.ToString());
        //     //System.Console.WriteLine("\n");
        // }

        return placeCollection.Items;       
    }

}