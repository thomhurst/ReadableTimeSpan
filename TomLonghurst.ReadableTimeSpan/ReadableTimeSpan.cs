using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using TomLonghurst.ReadableTimeSpan.Enums;
using TomLonghurst.ReadableTimeSpan.Extensions;
using TomLonghurst.ReadableTimeSpan.Mappers;

namespace TomLonghurst.ReadableTimeSpan;

public static class ReadableTimeSpan
{
    private static readonly string[] ValidSeparators = { ":", "+", "&", "|", "-", "and" };
    private static readonly Regex AlphaAndNumberRegex = new(@"(\d+\.?\d*)\s*([a-zA-Z]+)");
    private static bool _configurationBindingIsEnabled;

    public static void EnableConfigurationBinding()
    {
        if (_configurationBindingIsEnabled)
        {
            return;
        }
        
        _configurationBindingIsEnabled = true;
        TypeDescriptor.AddAttributes(typeof(TimeSpan), new TypeConverterAttribute(typeof(ReadableTimeSpanConverter)));
    }

    public static TimeSpan Parse(string stringTimespan)
    {
        return InternalParse(stringTimespan);
    }

    public static bool TryParse(string stringTimespan, out TimeSpan readableTimeSpan)
    {
        try
        {
            readableTimeSpan = InternalParse(stringTimespan);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private static TimeSpan InternalParse(string stringTimespan)
    {
        if (string.IsNullOrWhiteSpace(stringTimespan))
        {
            throw new ArgumentNullException(nameof(stringTimespan));
        }
        
        if (TimeSpan.TryParse(stringTimespan, out var timeSpan))
        {
            return timeSpan;
        }

        if (stringTimespan.Trim() == "0")
        {
            return TimeSpan.Zero;
        }
        
        timeSpan = TimeSpan.Zero;

        foreach (var segment in stringTimespan.ToLower(CultureInfo.InvariantCulture).Split(ValidSeparators, StringSplitOptions.RemoveEmptyEntries))
        {
            var (amount, unit) = ExtractUnitAndAmount(segment);
            timeSpan += ReadableTimeSpanUnitMapper.Map(unit).Invoke(amount);
        }

        return timeSpan;
    }

    public static string ToReadableString(this TimeSpan timeSpan)
    {
        var stringBuilder = new StringBuilder();

        if (timeSpan.Days > 1)
        {
            stringBuilder.Append($"{timeSpan.Days} Days, ");
        }
        else if (timeSpan.Days > 0)
        {
            stringBuilder.Append("1 Day, ");
        }
        
        if (timeSpan.Hours > 1)
        {
            stringBuilder.Append($"{timeSpan.Hours} Hours, ");
        }
        else if (timeSpan.Hours > 0)
        {
            stringBuilder.Append("1 Hour, ");
        }
        
        if (timeSpan.Minutes > 1)
        {
            stringBuilder.Append($"{timeSpan.Minutes} Minutes, ");
        }
        else if (timeSpan.Minutes > 0)
        {
            stringBuilder.Append("1 Minute, ");
        }
        
        if (timeSpan.Seconds > 1)
        {
            stringBuilder.Append($"{timeSpan.Seconds} Seconds, ");
        }
        else if (timeSpan.Seconds > 0)
        {
            stringBuilder.Append("1 Second, ");
        }
        
        if (timeSpan.Milliseconds > 1)
        {
            stringBuilder.Append($"{timeSpan.Milliseconds} Milliseconds, ");
        }
        else if (timeSpan.Milliseconds > 0)
        {
            stringBuilder.Append("1 Millisecond");
        }

        return stringBuilder
            .ToString()
            .TrimEnd(", ")
            .ReplaceLastOccurrence(", ", " and ");
    }

    private static (double amount, ReadableTimeSpanUnit unit) ExtractUnitAndAmount(string stringTimespan)
    {
        var regexResult = AlphaAndNumberRegex.Match(stringTimespan);

        if (regexResult.Groups.Count != 3) // The entire string itself counts as the first group.
        {
            throw new ArgumentException($"{stringTimespan} is not valid TimeSpan. Expected an amount and unit in each section.", nameof(stringTimespan));
        }

        var stringUnit = regexResult.Groups[2].Value;
        
        if (!Enum.TryParse(stringUnit, true, out ReadableTimeSpanUnit readableTimeSpanUnit))
        {
            throw new ArgumentException($"{stringUnit} is not a valid TimeSpan unit", nameof(ReadableTimeSpanUnit));
        }
        
        return (double.Parse(regexResult.Groups[1].Value), readableTimeSpanUnit);
    }
}