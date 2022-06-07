using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace TomLonghurst.ReadableTimeSpan.UnitTests;

public class OptionsTypeConversionTests
{
    [Test]
    public void Configuration_Binding()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("test_appsettings.json", optional: false)
            .Build();

        var testOptions = new TestOptions();
        configuration.Bind("TestOptions", testOptions);
        
        Assert.That(testOptions.OneDay.InnerTimeSpan.Days, Is.EqualTo(1));
        
        Assert.That(testOptions.ComplexTimeSpan.InnerTimeSpan.Days, Is.EqualTo(166));
        Assert.That(testOptions.ComplexTimeSpan.InnerTimeSpan.Hours, Is.EqualTo(13));
        Assert.That(testOptions.ComplexTimeSpan.InnerTimeSpan.Minutes, Is.EqualTo(42));
        Assert.That(testOptions.ComplexTimeSpan.InnerTimeSpan.Seconds, Is.EqualTo(29));
        Assert.That(testOptions.ComplexTimeSpan.InnerTimeSpan.Milliseconds, Is.EqualTo(324));
    }
    
    [Test]
    public void Configuration_GetValue()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("test_appsettings.json", optional: false)
            .Build();

        var complexTimeSpan = configuration.GetValue<ReadableTimeSpan>("TestOptions:ComplexTimeSpan");

        Assert.That(complexTimeSpan.InnerTimeSpan.Days, Is.EqualTo(166));
        Assert.That(complexTimeSpan.InnerTimeSpan.Hours, Is.EqualTo(13));
        Assert.That(complexTimeSpan.InnerTimeSpan.Minutes, Is.EqualTo(42));
        Assert.That(complexTimeSpan.InnerTimeSpan.Seconds, Is.EqualTo(29));
        Assert.That(complexTimeSpan.InnerTimeSpan.Milliseconds, Is.EqualTo(324));
    }
}