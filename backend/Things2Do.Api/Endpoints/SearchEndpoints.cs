namespace Things2Do.Api.Endpoints;

//Sets up endpoints for the searching function
public static class SearchEndpoints
{
    private static void search()
    {

    }

    public static RouteGroupBuilder MapSearchEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("search").WithParameterValidation();
        //group.MapPut("/{pos}", (s) => {});
    }
}