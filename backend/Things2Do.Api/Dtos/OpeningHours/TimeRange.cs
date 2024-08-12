using System.ComponentModel.DataAnnotations;

namespace Things2Do.Api.Dtos.OpeningHours;


public struct TimeRangeDto //NOTE -- may wanna move somewhere else lowk
{
    //If start == end ==> place open 24 hours
    public TimeOnly Start { get; set; }

    public TimeOnly End {get; set; }

    public TimeRangeDto(TimeOnly start, TimeOnly end)
    {
        Start = start;
        End = end;
    }
}