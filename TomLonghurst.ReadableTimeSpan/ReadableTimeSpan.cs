using System.Text;
using System.Text.RegularExpressions;
using TomLonghurst.ReadableTimeSpan.Enums;
using TomLonghurst.ReadableTimeSpan.Extensions;
using TomLonghurst.ReadableTimeSpan.Mappers;

namespace TomLonghurst.ReadableTimeSpan;

public readonly struct ReadableTimeSpan : IComparable, IComparable<ReadableTimeSpan>, IEquatable<ReadableTimeSpan>
{
    public TimeSpan InnerTimeSpan { get; }
    
    private static readonly string[] ValidSeparators = { ":", "|", "/", "-", "and" };
    private static readonly Regex AlphaAndNumberRegex = new(@"(\d+\.?\d*)\s*([a-zA-Z]+)");

    public static implicit operator TimeSpan(ReadableTimeSpan readableTimeSpan) => readableTimeSpan.InnerTimeSpan;
    public static implicit operator ReadableTimeSpan(TimeSpan timeSpan) => new ReadableTimeSpan(timeSpan);

    public ReadableTimeSpan(string stringTimespan)
    {
        if (string.IsNullOrWhiteSpace(stringTimespan))
        {
            throw new ArgumentNullException(nameof(stringTimespan));
        }
        
        if (TimeSpan.TryParse(stringTimespan, out var timeSpan))
        {
            InnerTimeSpan = timeSpan;
            return;
        }

        if (stringTimespan.Trim() == "0")
        {
            InnerTimeSpan = TimeSpan.Zero;
            return;
        }
        
        InnerTimeSpan = TimeSpan.Zero;

        foreach (var segment in stringTimespan.ToLower().Split(ValidSeparators, StringSplitOptions.RemoveEmptyEntries))
        {
            var (amount, unit) = ExtractUnitAndAmount(segment);
            InnerTimeSpan += ReadableTimeSpanUnitMapper.Map(unit).Invoke(amount);
        }
    }

    public ReadableTimeSpan(TimeSpan timeSpan)
    {
        InnerTimeSpan = timeSpan;
    }

    public static ReadableTimeSpan Parse(string stringTimespan)
    {
        return new ReadableTimeSpan(stringTimespan);
    }

    public static bool TryParse(string stringTimespan, out ReadableTimeSpan readableTimeSpan)
    {
        try
        {
            readableTimeSpan = new ReadableTimeSpan(stringTimespan);
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    public int CompareTo(object? obj)
    {
        if (obj is not ReadableTimeSpan readableTimeSpan)
        {
            return -1;
        }

        return InnerTimeSpan.CompareTo(readableTimeSpan.InnerTimeSpan);
    }

    public int CompareTo(ReadableTimeSpan other)
    {
        return InnerTimeSpan.CompareTo(other.InnerTimeSpan);
    }

    public bool Equals(ReadableTimeSpan other)
    {
        return InnerTimeSpan.Equals(other.InnerTimeSpan);
    }

    public override bool Equals(object? obj)
    {
        return obj is ReadableTimeSpan span && Equals(span);
    }

    public override int GetHashCode()
    {
        return InnerTimeSpan.GetHashCode();
    }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();

        if (InnerTimeSpan.Days > 1)
        {
            stringBuilder.Append($"{InnerTimeSpan.Days} Days, ");
        }
        else if (InnerTimeSpan.Days > 0)
        {
            stringBuilder.Append("1 Day, ");
        }
        
        if (InnerTimeSpan.Hours > 1)
        {
            stringBuilder.Append($"{InnerTimeSpan.Hours} Hours, ");
        }
        else if (InnerTimeSpan.Hours > 0)
        {
            stringBuilder.Append("1 Hour, ");
        }
        
        if (InnerTimeSpan.Minutes > 1)
        {
            stringBuilder.Append($"{InnerTimeSpan.Minutes} Minutes, ");
        }
        else if (InnerTimeSpan.Minutes > 0)
        {
            stringBuilder.Append("1 Minute, ");
        }
        
        if (InnerTimeSpan.Seconds > 1)
        {
            stringBuilder.Append($"{InnerTimeSpan.Seconds} Seconds, ");
        }
        else if (InnerTimeSpan.Seconds > 0)
        {
            stringBuilder.Append("1 Second, ");
        }
        
        if (InnerTimeSpan.Milliseconds > 1)
        {
            stringBuilder.Append($"{InnerTimeSpan.Milliseconds} Milliseconds, ");
        }
        else if (InnerTimeSpan.Milliseconds > 0)
        {
            stringBuilder.Append("1 Millisecond");
        }

        return stringBuilder
            .ToString()
            .TrimEnd(", ")
            .ReplaceLastOccurrence(", ", " and ");
    }

    public static bool operator ==(ReadableTimeSpan left, ReadableTimeSpan right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ReadableTimeSpan left, ReadableTimeSpan right)
    {
        return !(left == right);
    }

    public static bool operator <(ReadableTimeSpan left, ReadableTimeSpan right)
    {
        return left.InnerTimeSpan < right.InnerTimeSpan;
    }

    public static bool operator >(ReadableTimeSpan left, ReadableTimeSpan right)
    {
        return left.InnerTimeSpan > right.InnerTimeSpan;
    }
    
    public static bool operator ==(TimeSpan left, ReadableTimeSpan right)
    {
        return left.Equals(right.InnerTimeSpan);
    }

    public static bool operator !=(TimeSpan left, ReadableTimeSpan right)
    {
        return !(left == right.InnerTimeSpan);
    }

    public static bool operator <(TimeSpan left, ReadableTimeSpan right)
    {
        return left < right.InnerTimeSpan;
    }

    public static bool operator >(TimeSpan left, ReadableTimeSpan right)
    {
        return left > right.InnerTimeSpan;
    }
    
    public static bool operator ==(ReadableTimeSpan left, TimeSpan right)
    {
        return left.InnerTimeSpan.Equals(right);
    }

    public static bool operator !=(ReadableTimeSpan left, TimeSpan right)
    {
        return !(left.InnerTimeSpan == right);
    }

    public static bool operator <(ReadableTimeSpan left, TimeSpan right)
    {
        return left.InnerTimeSpan < right;
    }

    public static bool operator >(ReadableTimeSpan left, TimeSpan right)
    {
        return left.InnerTimeSpan > right;
    }

    public int Days => InnerTimeSpan.Days;
    public int Hours => InnerTimeSpan.Hours;
    public int Minutes => InnerTimeSpan.Minutes;
    public int Seconds => InnerTimeSpan.Seconds;
    public int Milliseconds => InnerTimeSpan.Milliseconds;
    public long Ticks => InnerTimeSpan.Ticks;

    public double TotalDays => InnerTimeSpan.TotalDays;
    public double TotalHours => InnerTimeSpan.TotalHours;
    public double TotalMinutes => InnerTimeSpan.TotalMinutes;
    public double TotalSeconds => InnerTimeSpan.TotalSeconds;
    public double TotalMilliseconds => InnerTimeSpan.TotalMilliseconds;

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