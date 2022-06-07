using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TomLonghurst.ReadableTimeSpan;

public class ReadableTimeSpanInitialiser
{
    [ModuleInitializer]
    public static void Initialise()
    {
        TypeDescriptor.AddAttributes(typeof(ReadableTimeSpan), new TypeConverterAttribute(typeof(ReadableTimeSpanConverter)));
    }
}