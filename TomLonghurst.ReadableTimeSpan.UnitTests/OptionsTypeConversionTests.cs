using System;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace TomLonghurst.ReadableTimeSpan.UnitTests;

public class OptionsTypeConversionTests
{
    [OneTimeSetUp]
    public void Setup()
    {
        ReadableTimeSpan.EnableConfigurationBinding();
    }
    
    [Test]
    public void Configuration_Binding()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("test_appsettings.json", optional: false)
            .Build();

        var testOptions = new TestOptions();
        configuration.Bind("TestOptions", testOptions);
        
        Assert.That(testOptions.OneDay.Days, Is.EqualTo(1));
        
        Assert.That(testOptions.ComplexTimeSpan.Days, Is.EqualTo(166));
        Assert.That(testOptions.ComplexTimeSpan.Hours, Is.EqualTo(13));
        Assert.That(testOptions.ComplexTimeSpan.Minutes, Is.EqualTo(42));
        Assert.That(testOptions.ComplexTimeSpan.Seconds, Is.EqualTo(29));
        Assert.That(testOptions.ComplexTimeSpan.Milliseconds, Is.EqualTo(324));
    }
    
    [Test]
    public void Configuration_GetValue()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("test_appsettings.json", optional: false)
            .Build();

        var complexTimeSpan = configuration.GetValue<TimeSpan>("TestOptions:ComplexTimeSpan");

        Assert.That(complexTimeSpan.Days, Is.EqualTo(166));
        Assert.That(complexTimeSpan.Hours, Is.EqualTo(13));
        Assert.That(complexTimeSpan.Minutes, Is.EqualTo(42));
        Assert.That(complexTimeSpan.Seconds, Is.EqualTo(29));
        Assert.That(complexTimeSpan.Milliseconds, Is.EqualTo(324));
    }
}