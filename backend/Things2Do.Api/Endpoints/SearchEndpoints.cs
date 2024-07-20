using Things2Do.Api.Dtos;
using Things2Do.Api.Services;

namespace Things2Do.Api.Endpoints;

//Sets up endpoints for the searching function
public static class SearchEndpoints
{
    // private static void search()
    // {

    // }

    public static RouteGroupBuilder MapSearchEndpoints(this WebApplication app)
    {
        //WithParameterValidation() from MinimalApis.Extensions package
        var group = app.MapGroup("search").WithParameterValidation(); 

        //Location + filter information transmitted in body
        group.MapPut("/", async (SearchPlaceDto searchQuery, HereService hereApi) => {
            
            try //This stuff not done yet
            {
                await hereApi.GetPlacesAsync(searchQuery.Lat, searchQuery.Lng);
            }
            catch (HttpRequestException e)
            {
                //NOTE -- may want to inform the client somehow?
                Console.WriteLine($"There was an error with the HTTP Request: {e.Message}");
                throw;
            }

        });

        return group!;
    }
}