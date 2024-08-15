namespace Things2Do.Api.Dtos.OpeningHours;

//Represents known hours of operation for a place during a given week
//If a particular timeRange is null, we assume the place is closed for that day of the week
public record class OpeningHoursResultDto
{
    public bool IsOpen { get; init; }

    public TimeRangeDto? Monday { get; set; }

    public TimeRangeDto? Tuesday { get; set; }

    public TimeRangeDto? Wednesday { get; set; }
    public TimeRangeDto? Thursday { get; set; }

    public TimeRangeDto? Friday { get; set; }

    public TimeRangeDto? Saturday { get; set; }

    public TimeRangeDto? Sunday { get; set; }

    public OpeningHoursResultDto(bool isOpen)
    {
        IsOpen = isOpen;        
    }
}