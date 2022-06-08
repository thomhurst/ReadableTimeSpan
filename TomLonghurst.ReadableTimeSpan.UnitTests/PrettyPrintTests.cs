using NUnit.Framework;

namespace TomLonghurst.ReadableTimeSpan.UnitTests;

public class PrettyPrintTests
{
    [Test]
    public void Test()
    {
        var readableTimeSpan = ReadableTimeSpan.Parse("166 days | 13 hrs | 42 min | 29 seconds | 324ms");
        
        Assert.That(readableTimeSpan.ToReadableString(), Is.EqualTo("166 Days, 13 Hours, 42 Minutes, 29 Seconds and 324 Milliseconds"));
    }
    
    [Test]
    public void Test2()
    {
        var readableTimeSpan = ReadableTimeSpan.Parse("166 days | 13 hrs | 324ms");
        
        Assert.That(readableTimeSpan.ToReadableString(), Is.EqualTo("166 Days, 13 Hours and 324 Milliseconds"));
    }
    
    [Test]
    public void Test3()
    {
        var readableTimeSpan = ReadableTimeSpan.Parse("166 days | 13 hrs");
        
        Assert.That(readableTimeSpan.ToReadableString(), Is.EqualTo("166 Days and 13 Hours"));
    }
    
    [Test]
    public void Test4()
    {
        var readableTimeSpan = ReadableTimeSpan.Parse("13 hrs");
        
        Assert.That(readableTimeSpan.ToReadableString(), Is.EqualTo("13 Hours"));
    }
}