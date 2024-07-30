using Things2Do.Api.Data.Deserialization;
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
        // POST /search
        group.MapPost("/", async (SearchPlaceDto searchQuery, HereService hereApi) => {
            
            //May export this into seperate method but this works
            try //This stuff not done yet
            {
                List<PlaceDeserialized> placesDeserialized = 
                    await hereApi.GetPlacesAsync(searchQuery.Lat, searchQuery.Lng);


                //Convert to a result DTO
                var places = placesDeserialized //Formatting goofy
                    .Where(pl => pl.ResultType == "place") //Filter out streets, towns, etc
                    .Select(pl => //map fn
                    {
                        //Pack data into contact dto
                        ContactResultDto? contacts;
                        if (pl.Contacts is null)
                        {
                            contacts = null;
                        }
                        else
                        {
                            var ctSource = pl.Contacts[0];
                            contacts = new ContactResultDto
                            (
                                Phone: ctSource.Phone is null ? 
                                       new List<string>() : ctSource.Phone.Select(c => c.Value),
                                Web: ctSource.Web is null ? 
                                       new List<string>() : ctSource.Web.Select(c => c.Value),
                                Email: ctSource.Email is null ? 
                                       new List<string>() : ctSource.Email.Select(c => c.Value)
                            );
                        }

                        //ContactResultDto contacts = new

                        return new PlaceResultDto(
                            Title: pl.Title,
                            Lat: pl.Position.Lat,
                            Lng: pl.Position.Lng,
                            Address: pl.Address.Label,
                            Contacts: contacts
                        );
                    });
                return Results.Ok(places);
                
                //FOR TESTING -- to see result of requests
                //return Results.Ok(placesDeserialized);

            }
            catch (HttpRequestException e)
            {
                //NOTE -- may want to inform the client somehow?
                Console.Error.WriteLine($"HTTP error: {e.Message}");
                return Results.BadRequest(); //FIXME Prob want to return something else
            }

        });

        return group!;
    }
}