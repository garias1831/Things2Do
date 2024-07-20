
namespace Things2Do.Api.Serivces;

//NOTE -- may want to store the api key here (access from somewhere private)
public class HereService
{
    private readonly HttpClient _httpClient;

    //TODO -- may want to have a seperate class OR option to use the browse vs discover endpoints
    //Browse has filtering but ranks based on distance whereas discover 
    //ranks based on relevance to a text query
    public HereService(HttpClient httpClient)
    {
        _httpClient = httpClient;

        _httpClient.BaseAddress = new Uri("https://browse.search.hereapi.com/v1/browse");
    }

    
}