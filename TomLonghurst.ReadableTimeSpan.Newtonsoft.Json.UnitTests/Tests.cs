using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;
using TomLonghurst.ReadableTimeSpan.UnitTests;

namespace TomLonghurst.ReadableTimeSpan.Newtonsoft.Json.UnitTests;

public class Tests
{
    [Test]
    public void Throws_Without_Custom_Converter()
    {
        var exception = Assert.Throws<JsonSerializationException>(() =>
            JsonConvert.DeserializeObject<TestOptions>(File.ReadAllText("test_appsettings.json")));
        
        Assert.That(exception.Message, Is.EqualTo("Error converting value \"1 day\" to type 'System.TimeSpan'. Path 'OneDay', line 2, position 21."));
    }
    
    [Test]
    public void Does_Not_Throw_With_Custom_Converter()
    {
        var testOptions = JsonConvert.DeserializeObject<TestOptions>(File.ReadAllText("test_appsettings.json"), new ReadableTimeSpanJsonConverter());

        Assert.That(testOptions.OneDay.Days, Is.EqualTo(1));
        
        Assert.That(testOptions.ComplexTimeSpan.Days, Is.EqualTo(166));
        Assert.That(testOptions.ComplexTimeSpan.Hours, Is.EqualTo(13));
        Assert.That(testOptions.ComplexTimeSpan.Minutes, Is.EqualTo(42));
        Assert.That(testOptions.ComplexTimeSpan.Seconds, Is.EqualTo(29));
        Assert.That(testOptions.ComplexTimeSpan.Milliseconds, Is.EqualTo(324));
    }
}