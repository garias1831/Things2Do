using System.Globalization;
using Serilog;
using Things2Do.Api.Data.Deserialization;
using Things2Do.Api.Dtos.OpeningHours;

namespace Things2Do.Api.Dtos.Factories;

//Should create create components for the place result dto
//From the deserialized JSON response
//Think Contacts, opening hours, etc
public static class PlaceResultInfoFactory
{    

    public static ContactResultDto? CreateContactDto(PlaceDeserialized place)
    {
        if (place.Contacts is null) //No contact data in response
        {
            return null;
        }
        
        var ctSource = place.Contacts[0];
        return new ContactResultDto
        (
            Phone: ctSource.Phone is null ? 
                    new List<string>() : ctSource.Phone.Select(c => c.Value),
            Web: ctSource.Web is null ? 
                    new List<string>() : ctSource.Web.Select(c => c.Value),
            Email: ctSource.Email is null ? 
                    new List<string>() : ctSource.Email.Select(c => c.Value)
        );
        
    }

    public static OpeningHoursResultDto? CreateOpeningHoursDto(PlaceDeserialized place)
    {
        //TODO -- get client timezone info and pass it here to render times

        
        if (place.OpeningHours is null) //No contact data in response
        {
            return null;
        }

        var hoursSource = place.OpeningHours[0];
        OpeningHoursResultDto dto = new OpeningHoursResultDto(hoursSource.IsOpen);

        //Loop thru icalendar entries and map to days in the dto
        foreach(TimeEntryDeserialized icalEntry in hoursSource.Values)
        {
            string startString = icalEntry.Start.Substring(1); //cut out the 'T'
            TimeOnly start = TimeOnly.ParseExact(startString, "HHmmss", CultureInfo.InvariantCulture);

            string durationHours = icalEntry.Duration.Substring(2, 2);
            
            TimeRange range;
            if (durationHours == "24")
            {
                //Mark as open 24 for hours   
                TimeOnly end = start;
                range = new TimeRange(start, end);
            }
            else
            {
                string durationString = icalEntry.Duration.Substring(2); //Cut out the 'PT'
                TimeOnly duration = TimeOnly.ParseExact(durationString, @"HH\Hmm\M");
                TimeOnly end = start.Add(duration.ToTimeSpan());

                range = new TimeRange(start, end);

                //Log.Information($"Title: {place.Title}, Hours: {start}-{end}");
            }
            //Remove the 'FREQ:DAILY;BYDAY:' to get comma-sep str of abbreviated days
            string daysStr = icalEntry.Recurrence.Substring(17);

            
            //Map the time range to the properties on the OpeningHoursDTO
            foreach (string day in daysStr.Split(","))
            {
                //Simple but works fine
                switch (day)
                {
                    case "MO":
                        dto.Monday = range;
                        break;
                    case "TU":
                        dto.Tuesday = range;
                        break;
                    case "WE":
                        dto.Wednesday = range;
                        break;
                    case "TH":
                        dto.Thursday = range;
                        break;
                    case "FR":
                        dto.Friday = range;
                        break;
                    case "SA":
                        dto.Saturday = range;
                        break;
                    case "SU":
                        dto.Sunday = range;
                        break;
                }
            }
        }

        return dto;
    }

    
}