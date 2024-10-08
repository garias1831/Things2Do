using System.Net.Http.Headers;
using Things2Do.Api.Data.Deserialization;
using Things2Do.Api.Data.Here;
using Things2Do.Api.Dtos;
using Things2Do.Api.Filtering;

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
        
        _apiKey = Environment.GetEnvironmentVariable("HERE_KEY")!;
    }

    public async Task<List<PlaceDeserialized>> GetPlacesAsync(SearchPlaceDto request)
    {
        //NOTE -- the lang=en param here returns place names, address, etc in ENGLISH
        //For future may want to find lang of page and shove it in here + render days of week in local language / culture
        //Or render in client lang w/ subtitle of native lang
        
        var filtering = new PrePlaceFiltering();

        decimal lat = request.Lat;
        decimal lng = request.Lng;
        int searchRadius = request.Filters.Distance; //in meters
        string categoryCodes = filtering.GetPlaceCategoryCodes(request.Filters.TypeFilters);

        var placeCollection = await _httpClient.GetFromJsonAsync<PlaceCollectionDeserialized>(
            String.Concat(
                $"?at={lat},{lng}",
                $"&categories={categoryCodes}",
                $"&in=circle:{lat},{lng};r={searchRadius}",
                $"&lang=en&apiKey={_apiKey}"
            )
        );
        
        if (placeCollection is null)
        {
            return new List<PlaceDeserialized>();
        }

        return placeCollection.Items;       
    }

}