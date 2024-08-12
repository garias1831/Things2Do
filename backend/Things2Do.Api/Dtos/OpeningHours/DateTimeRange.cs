using System.ComponentModel.DataAnnotations;

namespace Things2Do.Api.Dtos.OpeningHours; //NOt sure if this should go here

public record struct DateTimeRangeDto //NOTE -- may wanna move somewhere else lowk
(
    [Required]
    DateTime Start,

    [Required]
    DateTime End,

    [Required] [Range(-720, 840)]
    int TimeZoneOffset //How far ahead / behind client time is from UTC in minutes
);