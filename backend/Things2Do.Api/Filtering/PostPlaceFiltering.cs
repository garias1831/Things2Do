using Things2Do.Api.Dtos;
using Things2Do.Api.Dtos.OpeningHours;

namespace Things2Do.Api.Filtering;
//Not sure thtat this needs its own namespace tbh, but its whateavserftcd

//Filter results AFTER fetching an api response
//Methods here should behave like predicates
public class PostPlaceFiltering
{
    //Filter based on the opening hours the user has set and those present in the given dto
    public bool IsOpenBetween(SearchPlaceDto request, PlaceResultDto dto)
    {
        //No opening hours section we just return
        //TODO tho -> should make this toggleable by the client
        if (dto.OpeningHours is null)
        {
            return true;
        }

        //Start and end times as client time
        int off = request.Filters.Hours.TimeZoneOffset;
        DateTime start = request.Filters.Hours.Start.AddMinutes(off);
        DateTime end = request.Filters.Hours.End.AddMinutes(off);

        if (dto.OpeningHours is null ||start == end)
        {
            return true;
        }

        //Get the fullname day of the week
        string startDay = start.ToString("dddd");
        
        Type hoursDtoType = typeof(OpeningHoursResultDto);
        
        //Convert timeonly to datetime using datetime for date vals
        DateTime toDateTime(TimeOnly t, DateTime dateReference)
        {
            //This is so js i hate it
            return new DateTime(
                dateReference.Year,
                dateReference.Month,
                dateReference.Day,
                t.Hour,
                t.Minute,
                t.Second
            );
        }
    
        var tempHrs = hoursDtoType.GetProperty(startDay)!.GetValue(dto.OpeningHours);
        
        //Null here means that the place is closed on the day the user wants to check if open
        if (tempHrs is null)
        {
            return false;
        }

        TimeRangeDto placeHours = (TimeRangeDto)tempHrs;
        
        //If the hours are 24 / 7 and the client asks for 24 / 7 hours
        if (placeHours.Start == placeHours.End && start == end)
        {
            return true;
        }
    
        return toDateTime(placeHours.Start, start) <= start && 
                end <= toDateTime(placeHours.End, end);
    }
}