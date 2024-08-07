namespace Things2Do.Api.Dtos.OpeningHours;

//Represents known hours of operation for a place during a given week
//If a particular timeRange is null, we assume the place is closed for that day of the week
public record class OpeningHoursResultDto
{
    public bool IsOpen { get; init; }

    public TimeRange? Monday { get; set; }

    public TimeRange? Tuesday { get; set; }

    public TimeRange? Wednesday { get; set; }
    public TimeRange? Thursday { get; set; }

    public TimeRange? Friday { get; set; }

    public TimeRange? Saturday { get; set; }

    public TimeRange? Sunday { get; set; }

    public OpeningHoursResultDto(bool isOpen)
    {
        IsOpen = isOpen;        
    }
}