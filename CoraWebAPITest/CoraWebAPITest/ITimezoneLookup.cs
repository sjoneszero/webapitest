using System;

public interface ITimezoneLookup
{
	(bool isValid, string timezone) GetTimezone(string timezone);

}
