using System;

public class TimezoneLookup : ITimezoneLookup
{
		public TimeZoneInfo c_Timezone;

		public (bool isValid, string timezone) GetTimezone(string timezone)
		{
			if(timezone == null) { return (false, null); }

        try
        {
			c_Timezone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
		}
        catch (TimeZoneNotFoundException)
        {

			if (c_Timezone == null) { return (false, null); }
		}
			
			return (true, c_Timezone.DisplayName);
	}

}
