using System.ComponentModel.DataAnnotations;

namespace Things2Do.Api.Dtos.OpeningHours; //NOt sure if this should go here

public struct DateTimeRange //NOTE -- may wanna move somewhere else lowk
{
    //If start == end ==> place open 24 hours
    public DateTime Start { get; set; }

    public DateTime End {get; set; }

    public DateTimeRange(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    }
}