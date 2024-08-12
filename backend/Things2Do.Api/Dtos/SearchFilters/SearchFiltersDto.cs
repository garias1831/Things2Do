using System.ComponentModel.DataAnnotations;
using Things2Do.Api.Dtos.OpeningHours;

namespace Things2Do.Api.Dtos.SearchFilters;

public record class SearchFiltersDto
(
    [Required] [Range(1609, 80467)] //1 - 50 miles, or to display 2km to 80km
    int Distance, //distance in meters to search

    //Filter places based on whether they are open on a particular date (the start time is during client's current day of the week)
    //Places w/ no opening hours information included by default

    //TODO may want to put time stuff in its own class
    [Required]
    bool RequireHoursInfo,

    //DateTime ranges in UTC (default) with a timezone offset for convertin
    [Required] 
    DateTimeRangeDto Hours,

    [Required]
    TypeFiltersDto TypeFilters
);