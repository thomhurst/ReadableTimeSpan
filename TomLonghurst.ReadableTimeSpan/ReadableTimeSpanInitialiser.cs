using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TomLonghurst.ReadableTimeSpan;

internal static class ReadableTimeSpanInitialiser
{
    [ModuleInitializer]
    public static void Initialise()
    {
        TypeDescriptor.AddAttributes(typeof(ReadableTimeSpan), new TypeConverterAttribute(typeof(ReadableTimeSpanConverter)));
    }
}