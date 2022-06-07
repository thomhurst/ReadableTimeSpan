namespace TomLonghurst.ReadableTimeSpan.Extensions;

internal static class StringExtensions
{
    internal static string TrimEnd(this string source, string value)
    {
        return source.EndsWith(value) 
            ? source.Remove(source.LastIndexOf(value, StringComparison.Ordinal)) 
            : source;
    }
    
    internal static string ReplaceLastOccurrence(this string source, string find, string replace)
    {
        var index = source.LastIndexOf(find, StringComparison.Ordinal);

        if (index == -1)
        {
            return source;
        }

        return source.Remove(index, find.Length).Insert(index, replace);
    }
}