using TomLonghurst.ReadableTimeSpan.Enums;

namespace TomLonghurst.ReadableTimeSpan.Mappers;

public static class ReadableTimeSpanUnitMapper
{
    public static Func<double, TimeSpan> Map(ReadableTimeSpanUnit readableTimeSpanUnit)
    {
        switch (readableTimeSpanUnit)
        {
            case ReadableTimeSpanUnit.T:
            case ReadableTimeSpanUnit.Tick:
            case ReadableTimeSpanUnit.Ticks:
                return d => TimeSpan.FromTicks((long) d);
                
            case ReadableTimeSpanUnit.Ms:
            case ReadableTimeSpanUnit.Mil:
            case ReadableTimeSpanUnit.Mils:
            case ReadableTimeSpanUnit.Milli:
            case ReadableTimeSpanUnit.Millis:
            case ReadableTimeSpanUnit.Millisecond:
            case ReadableTimeSpanUnit.Milliseconds:
                return TimeSpan.FromMilliseconds;
                
            case ReadableTimeSpanUnit.S:
            case ReadableTimeSpanUnit.Sec:
            case ReadableTimeSpanUnit.Secs:
            case ReadableTimeSpanUnit.Second:
            case ReadableTimeSpanUnit.Seconds:
                return TimeSpan.FromSeconds;
            
            case ReadableTimeSpanUnit.M:
            case ReadableTimeSpanUnit.Min:
            case ReadableTimeSpanUnit.Mins:
            case ReadableTimeSpanUnit.Minute:
            case ReadableTimeSpanUnit.Minutes:
                return TimeSpan.FromMinutes;
            
            case ReadableTimeSpanUnit.H:
            case ReadableTimeSpanUnit.Hr:
            case ReadableTimeSpanUnit.Hrs:
            case ReadableTimeSpanUnit.Hour:
            case ReadableTimeSpanUnit.Hours:
                return TimeSpan.FromHours;
            
            case ReadableTimeSpanUnit.D:
            case ReadableTimeSpanUnit.Day:
            case ReadableTimeSpanUnit.Days:
                return TimeSpan.FromDays;
            
            default:
                throw new ArgumentOutOfRangeException(nameof(readableTimeSpanUnit), readableTimeSpanUnit, null);
        }
    }
}