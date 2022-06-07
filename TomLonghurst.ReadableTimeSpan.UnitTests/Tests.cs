using NUnit.Framework;

namespace TomLonghurst.ReadableTimeSpan.UnitTests;

public class Tests
{
    [TestCase("|")]
    [TestCase(":")]
    [TestCase("/")]
    [TestCase("-")]
    [TestCase("and")]
    public void SeparatorsAreValid(string separator)
    {
        var timeSpan = new ReadableTimeSpan($"5 days {separator} 4 hours {separator} 3 minutes {separator} 2 seconds {separator} 1 millisecond");

        Assert.That(timeSpan.InnerTimeSpan.Days, Is.EqualTo(5));
        Assert.That(timeSpan.InnerTimeSpan.Hours, Is.EqualTo(4));
        Assert.That(timeSpan.InnerTimeSpan.Minutes, Is.EqualTo(3));
        Assert.That(timeSpan.InnerTimeSpan.Seconds, Is.EqualTo(2));
        Assert.That(timeSpan.InnerTimeSpan.Milliseconds, Is.EqualTo(1));
    }

    [TestCase("d")]
    [TestCase("D")]
    [TestCase("day")]
    [TestCase("Day")]
    [TestCase("DAY")]
    [TestCase("days")]
    [TestCase("Days")]
    [TestCase("DAYS")]
    public void CanParseDifferentWordsFor_Days(string word)
    {
        var timeSpan = new ReadableTimeSpan($"5 {word} | 4 hours | 3 minutes | 2 seconds | 1 millisecond");
        
        Assert.That(timeSpan.InnerTimeSpan.Days, Is.EqualTo(5));
    }

    [TestCase("h")]
    [TestCase("H")]
    [TestCase("hr")]
    [TestCase("Hr")]
    [TestCase("HR")]
    [TestCase("hrs")]
    [TestCase("Hrs")]
    [TestCase("HRS")]
    [TestCase("hour")]
    [TestCase("Hour")]
    [TestCase("HOUR")]
    [TestCase("hours")]
    [TestCase("Hours")]
    [TestCase("HOURS")]
    public void CanParseDifferentWordsFor_Hours(string word)
    {
        var timeSpan = new ReadableTimeSpan($"5 days | 4 {word} | 3 minutes | 2 seconds | 1 millisecond");
        
        Assert.That(timeSpan.InnerTimeSpan.Hours, Is.EqualTo(4));
    }
    
    [TestCase("m")]
    [TestCase("M")]
    [TestCase("min")]
    [TestCase("Min")]
    [TestCase("MIN")]
    [TestCase("mins")]
    [TestCase("Mins")]
    [TestCase("MINS")]
    [TestCase("minute")]
    [TestCase("Minute")]
    [TestCase("MINUTE")]
    [TestCase("minutes")]
    [TestCase("Minutes")]
    [TestCase("MINUTES")]
    public void CanParseDifferentWordsFor_Minutes(string word)
    {
        var timeSpan = new ReadableTimeSpan($"5 days | 4 hours | 3 {word} | 2 seconds | 1 millisecond");
        
        Assert.That(timeSpan.InnerTimeSpan.Minutes, Is.EqualTo(3));
    }
    
    [TestCase("s")]
    [TestCase("S")]
    [TestCase("sec")]
    [TestCase("Sec")]
    [TestCase("SEC")]
    [TestCase("secs")]
    [TestCase("Secs")]
    [TestCase("SECS")]
    [TestCase("second")]
    [TestCase("Second")]
    [TestCase("SECOND")]
    [TestCase("seconds")]
    [TestCase("Seconds")]
    [TestCase("SECONDS")]
    public void CanParseDifferentWordsFor_Seconds(string word)
    {
        var timeSpan = new ReadableTimeSpan($"5 days | 4 hours | 3 minutes | 2 {word} | 1 millisecond");
        
        Assert.That(timeSpan.InnerTimeSpan.Seconds, Is.EqualTo(2));
    }
    
    [TestCase("ms")]
    [TestCase("Ms")]
    [TestCase("MS")]
    [TestCase("mil")]
    [TestCase("Mil")]
    [TestCase("MIL")]
    [TestCase("mils")]
    [TestCase("Mils")]
    [TestCase("MILS")]
    [TestCase("milli")]
    [TestCase("Milli")]
    [TestCase("MILLI")]
    [TestCase("millis")]
    [TestCase("Millis")]
    [TestCase("MILLIS")]
    [TestCase("millisecond")]
    [TestCase("Millisecond")]
    [TestCase("MILLISECOND")]
    [TestCase("milliseconds")]
    [TestCase("Milliseconds")]
    [TestCase("MILLISECONDS")]
    public void CanParseDifferentWordsFor_Milliseconds(string word)
    {
        var timeSpan = new ReadableTimeSpan($"5 days | 4 hours | 3 minutes | 2 seconds | 324 {word}");
        
        Assert.That(timeSpan.InnerTimeSpan.Milliseconds, Is.EqualTo(324));
    }
    
    [TestCase("t")]
    [TestCase("T")]
    [TestCase("tick")]
    [TestCase("Tick")]
    [TestCase("TICK")]
    [TestCase("ticks")]
    [TestCase("Ticks")]
    [TestCase("TICKS")]
    public void CanParseDifferentWordsFor_Ticks(string word)
    {
        var timeSpan = new ReadableTimeSpan($"736736 {word}");
        
        Assert.That(timeSpan.InnerTimeSpan.Ticks, Is.EqualTo(736736));
    }

    [Test]
    public void CanParseDecimals_Days()
    {
        var timeSpan = new ReadableTimeSpan($"5.5 days");
        
        Assert.That(timeSpan.InnerTimeSpan.Days, Is.EqualTo(5));
        Assert.That(timeSpan.InnerTimeSpan.Hours, Is.EqualTo(12));
    }
    
    [Test]
    public void CanParseDecimals_Hours()
    {
        var timeSpan = new ReadableTimeSpan($"5.5 hours");
        
        Assert.That(timeSpan.InnerTimeSpan.Hours, Is.EqualTo(5));
        Assert.That(timeSpan.InnerTimeSpan.Minutes, Is.EqualTo(30));
    }
    
    [Test]
    public void CanParseDecimals_Minutes()
    {
        var timeSpan = new ReadableTimeSpan($"5.5 mins");
        
        Assert.That(timeSpan.InnerTimeSpan.Minutes, Is.EqualTo(5));
        Assert.That(timeSpan.InnerTimeSpan.Seconds, Is.EqualTo(30));
    }
    
    [Test]
    public void CanParseDecimals_Seconds()
    {
        var timeSpan = new ReadableTimeSpan($"5.5 secs");
        
        Assert.That(timeSpan.InnerTimeSpan.Seconds, Is.EqualTo(5));
        Assert.That(timeSpan.InnerTimeSpan.Milliseconds, Is.EqualTo(500));
    }
    
    [Test]
    public void CanParseSurroundingSpaces()
    {
        var timeSpan = new ReadableTimeSpan("   5  days   and 4hr |     3m  |   2s     and   324ms   ");
        
        Assert.That(timeSpan.InnerTimeSpan.Days, Is.EqualTo(5));
        Assert.That(timeSpan.InnerTimeSpan.Hours, Is.EqualTo(4));
        Assert.That(timeSpan.InnerTimeSpan.Minutes, Is.EqualTo(3));
        Assert.That(timeSpan.InnerTimeSpan.Seconds, Is.EqualTo(2));
        Assert.That(timeSpan.InnerTimeSpan.Milliseconds, Is.EqualTo(324));
    }
    
    [Test]
    public void CanParse_Zero()
    {
        var timeSpan = new ReadableTimeSpan("0");

        Assert.That(timeSpan.InnerTimeSpan.TotalMilliseconds, Is.EqualTo(0));
    }
    
    [Test]
    public void CanParse_ZeroSeconds()
    {
        var timeSpan = new ReadableTimeSpan("0 seconds");

        Assert.That(timeSpan.InnerTimeSpan.TotalMilliseconds, Is.EqualTo(0));
    }
}