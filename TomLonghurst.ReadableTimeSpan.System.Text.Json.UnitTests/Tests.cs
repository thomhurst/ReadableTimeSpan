using System;
using System.IO;
using System.Text.Json;
using NUnit.Framework;

namespace TomLonghurst.ReadableTimeSpan.System.Text.Json.UnitTests;

public class Tests
{
    [Test]
    public void Throws_Without_Custom_Converter()
    {
        var exception = Assert.Throws<JsonException>(() =>
            JsonSerializer.Deserialize<TestOptions>(File.ReadAllText("test_appsettings.json")));
        
        Assert.That(exception.Message, Is.EqualTo("The JSON value could not be converted to System.TimeSpan. Path: $.OneDay | LineNumber: 1 | BytePositionInLine: 21."));
    }
    
    [Test]
    public void Does_Not_Throw_With_Custom_Converter()
    {
        var testOptions = JsonSerializer.Deserialize<TestOptions>(File.ReadAllText("test_appsettings.json"), new JsonSerializerOptions
        {
            Converters = { new ReadableTimeSpanJsonConverter() }
        });

        Assert.That(testOptions.OneDay.Days, Is.EqualTo(1));
        
        Assert.That(testOptions.ComplexTimeSpan.Days, Is.EqualTo(166));
        Assert.That(testOptions.ComplexTimeSpan.Hours, Is.EqualTo(13));
        Assert.That(testOptions.ComplexTimeSpan.Minutes, Is.EqualTo(42));
        Assert.That(testOptions.ComplexTimeSpan.Seconds, Is.EqualTo(29));
        Assert.That(testOptions.ComplexTimeSpan.Milliseconds, Is.EqualTo(324));
    }
}