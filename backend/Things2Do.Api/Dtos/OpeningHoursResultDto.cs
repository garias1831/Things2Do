namespace Things2Do.Api.Dtos;

//Represents known hours of operation for a business during a given week
public record class OpeningHoursResultDto
(
    bool IsOpen,

    TimeOnly Monday,

    TimeOnly Tuesday,

    TimeOnly Wednesday,

    TimeOnly Thursday,

    TimeOnly Friday,

    TimeOnly Saturday,

    TimeOnly Sunday
);