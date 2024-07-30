namespace Things2Do.Api.Dtos;


//Holds contact info 2 be rendered on screen
public record class ContactResultDto
(
    IEnumerable<string> Phone,
    IEnumerable<string> Web,
    IEnumerable<string> Email
);